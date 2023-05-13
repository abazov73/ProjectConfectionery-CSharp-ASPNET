using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.DI;
using ConfectioneryContracts.SearchModels;
using ConfectioneryDataModels.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Confectionery
{
    public partial class FormPastry : Form
    {
        private readonly ILogger _logger;
        private readonly IPastryLogic _logic;
        private int? _id;
        private Dictionary<int, (IIngredientModel, int)> _pastryIngredients;
        public int Id { set { _id = value; } }

        public FormPastry(ILogger<FormPastry> logger, IPastryLogic logic)
        {
            InitializeComponent();
            _logger = logger;
            _logic = logic;
            _pastryIngredients = new Dictionary<int, (IIngredientModel, int)>();
        }

        private void FormPastry_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                _logger.LogInformation("Загрузка изделия");
                try
                {
                    var view = _logic.ReadElement(new PastrySearchModel { Id = _id.Value });
                    if (view != null)
                    {
                        textBoxName.Text = view.PastryName;
                        textBoxPrice.Text = view.Price.ToString();
                        _pastryIngredients = view.PastryIngredients ?? new Dictionary<int, (IIngredientModel, int)>();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка загрузки изделия");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadData()
        {
            _logger.LogInformation("Загрузка ингредиентов изделия");
            try
            {
                if (_pastryIngredients != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in _pastryIngredients)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1.IngredientName, pc.Value.Item2 });
                    }
                    textBoxPrice.Text = CalcPrice().ToString();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки ингредиентов изделия");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var service = DependencyManager.Instance.Resolve<FormPastryIngredient>();
            if (service is FormPastryIngredient form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.IngredientModel == null)
                    {
                        return;
                    }
                    _logger.LogInformation("Добавление нового ингредиента: {IngredientName} - {Count}", form.IngredientModel.IngredientName, form.Count);
                    if (_pastryIngredients.ContainsKey(form.Id))
                    {
                        _pastryIngredients[form.Id] = (form.IngredientModel, form.Count);
                    }
                    else
                    {
                        _pastryIngredients.Add(form.Id, (form.IngredientModel, form.Count));
                    }
                    LoadData();
                }
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var service = DependencyManager.Instance.Resolve<FormPastryIngredient>();
                if (service is FormPastryIngredient form)
                {
                    int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                    form.Id = id;
                    form.Count = _pastryIngredients[id].Item2;
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.IngredientModel == null)
                        {
                            return;
                        }
                        _logger.LogInformation("Изменение ингредиента: {IngredientName} - {Count}", form.IngredientModel.IngredientName, form.Count);
                        _pastryIngredients[form.Id] = (form.IngredientModel, form.Count);
                        LoadData();
                    }
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _logger.LogInformation("Удаление ингредиента: {IngredientName} - {Count}", dataGridView.SelectedRows[0].Cells[1].Value);
                        _pastryIngredients?.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_pastryIngredients == null || _pastryIngredients.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.LogInformation("Сохранение изделия");
            try
            {
                var model = new PastryBindingModel
                {
                    Id = _id ?? 0,
                    PastryName = textBoxName.Text,
                    Price = Convert.ToDouble(textBoxPrice.Text),
                    PastryIngredients = _pastryIngredients
                };
                var operationResult = _id.HasValue ? _logic.Update(model) : _logic.Create(model);
                if (!operationResult)
                {
                    throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка сохранения изделия");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private double CalcPrice()
        {
            double price = 0;
            foreach (var elem in _pastryIngredients)
            {
                price += ((elem.Value.Item1?.Cost ?? 0) * elem.Value.Item2);
            }
            return Math.Round(price * 1.1, 2);
        }
    }
}
