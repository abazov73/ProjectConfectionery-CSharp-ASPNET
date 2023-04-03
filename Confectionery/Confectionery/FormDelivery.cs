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
using System.Collections;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.BindingModels;

namespace Confectionery
{
    public partial class FormDelivery : Form
    {
        private readonly ILogger _logger;
        private readonly IPastryLogic _logicP;
        private readonly IShopLogic _logicS;
        private List<PastryViewModel>? _listPastry;
        private List<ShopViewModel>? _listShop;
        public FormDelivery(ILogger<FormDelivery> logger, IPastryLogic logicP, IShopLogic logicS)
        {
            InitializeComponent();
            _logger = logger;
            _logicP = logicP;
            _logicS = logicS;
        }

        private void FormDelivery_Load(object sender, EventArgs e)
        {
            _logger.LogInformation("Загрузка магазинов для поставки");
            _listShop = _logicS.ReadList(null);
            if (_listShop != null)
            {
                comboBoxShop.DisplayMember = "ShopName";
                comboBoxShop.ValueMember = "Id";
                comboBoxShop.DataSource = _listShop;
                comboBoxShop.SelectedItem = null;
            }
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (comboBoxShop.SelectedValue == null)
            {
                MessageBox.Show("Выберите магазин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
                ShopBindingModel shop = new ShopBindingModel
                {
                    Id = Convert.ToInt32(comboBoxShop.SelectedValue)
                };
                PastryBindingModel pastry = new PastryBindingModel
                {
                    Id = Convert.ToInt32(comboBoxPastry.SelectedValue),
                    PastryName = comboBoxPastry.Text
                };
                int count = Convert.ToInt32(textBoxSum.Text);
                bool operationResult = _logicS.DeliverPastryToShop(shop, pastry, count);
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
    }
}
