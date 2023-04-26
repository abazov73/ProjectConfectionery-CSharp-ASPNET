using ConfectioneryContracts.BusinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.BindingModels;
using ConfectioneryBusinessLogic.BusinessLogics;
using ConfectioneryDataModels.Models;

namespace Confectionery
{
    public partial class FormShop : Form
    {
        private readonly ILogger _logger;
        private readonly IShopLogic _logic;
        private Dictionary<int, (IPastryModel, int)> _shopPastries = new Dictionary<int, (IPastryModel, int)>();

        private int? _id;
        public int Id { set { _id = value; } }
        public FormShop(ILogger<FormShop> logger, IShopLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            _logger = logger;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxAdress.Text))
            {
                MessageBox.Show("Заполните адрес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCapacity.Text))
            {
                MessageBox.Show("Заполните вместимость", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.LogInformation("Сохранение магазина");
            try
            {
                var model = new ShopBindingModel
                {
                    Id = _id ?? 0,
                    ShopName = textBoxName.Text,
                    ShopAdress = textBoxAdress.Text,
                    OpeningDate = openingDateTimePicker.Value.Date,
                    PastryCapacity = Convert.ToInt32(textBoxCapacity.Text),
                    ShopPastries = _shopPastries
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
                _logger.LogError(ex, "Ошибка сохранения компонента");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormShop_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                try
                {
                    _logger.LogInformation("Получение магазина");
                    var view = _logic.ReadElement(new ShopSearchModel { Id = _id.Value });
                    if (view != null)
                    {
                        textBoxName.Text = view.ShopName;
                        textBoxAdress.Text = view.ShopAdress;
                        openingDateTimePicker.Value = view.OpeningDate;
                        textBoxCapacity.Text = view.PastryCapacity.ToString();
                        _shopPastries = view.ShopPastries;
                        foreach (var pc in view.ShopPastries)
                        {
                            dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1.PastryName, pc.Value.Item2 });
                        }
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка получения компонента");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                _logger.LogInformation("Загрузка заказов");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
