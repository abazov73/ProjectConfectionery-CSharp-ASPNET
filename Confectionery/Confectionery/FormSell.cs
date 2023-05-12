using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.ViewModels;
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
    public partial class FormSell : Form
    {
        private readonly ILogger _logger;
        private readonly IPastryLogic _logicP;
        private readonly IShopLogic _logicS;
        private List<PastryViewModel>? _listPastry;
        public FormSell(ILogger<FormSell> logger, IPastryLogic logicP, IShopLogic logicS)
        {
            InitializeComponent();
            _logger = logger;
            _logicP = logicP;
            _logicS = logicS;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (comboBoxPastry.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(textBoxSum.Text))
            {
                MessageBox.Show("Заполните поле количество!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int count = Convert.ToInt32(textBoxSum.Text);
                bool operationResult = _logicS.Sell(Convert.ToInt32(comboBoxPastry.SelectedValue), count);
                if (!operationResult)
                {
                    throw new Exception("Ошибка при создании поставки. Дополнительная информация в логах.");
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания поставки!");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormSell_Load(object sender, EventArgs e)
        {
            _logger.LogInformation("Загрузка изделий для поставки");
            _listPastry = _logicP.ReadList(null);
            if (_listPastry != null)
            {
                comboBoxPastry.DisplayMember = "PastryName";
                comboBoxPastry.ValueMember = "Id";
                comboBoxPastry.DataSource = _listPastry;
                comboBoxPastry.SelectedItem = null;
            }
        }
    }
}
