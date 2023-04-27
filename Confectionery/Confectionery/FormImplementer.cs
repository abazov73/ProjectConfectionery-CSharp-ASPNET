using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
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
    public partial class FormImplementer : Form
    {
        private readonly ILogger _logger;
        private readonly IImplementerLogic _logic;
        private int? _id;
        public int Id { set { _id = value; } }
        public FormImplementer(ILogger<FormImplementer> logger, IImplementerLogic logic)
        {
            InitializeComponent();
            _logger = logger;
            _logic = logic;
        }

        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                try
                {
                    _logger.LogInformation("Получение компонента");
                    var view = _logic.ReadElement(new ImplementerSearchModel { Id = _id.Value });
                    if (view != null)
                    {
                        textBoxFIO.Text = view.ImplementerFIO;
                        textBoxPassword.Text = view.Password;
                        textBoxWorkExperience.Text = view.WorkExperience.ToString();
                        textBoxQualification.Text = view.Qualification.ToString();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка получения компонента");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxWorkExperience.Text))
            {
                MessageBox.Show("Заполните опыт работы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxQualification.Text))
            {
                MessageBox.Show("Заполните опыт квалификацию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _logger.LogInformation("Сохранение исполнителя");
            try
            {
                var model = new ImplementerBindingModel
                {
                    Id = _id ?? 0,
                    ImplementerFIO = textBoxFIO.Text,
                    Password = textBoxPassword.Text,
                    WorkExperience = Convert.ToInt32(textBoxWorkExperience.Text),
                    Qualification = Convert.ToInt32(textBoxQualification.Text)
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
                _logger.LogError(ex, "Ошибка сохранения исполнителя");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
