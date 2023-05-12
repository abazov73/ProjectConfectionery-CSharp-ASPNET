using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
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
    public partial class FormMain : Form
    {
        private readonly ILogger _logger;
        private readonly IOrderLogic _orderLogic;

        public FormMain(ILogger<FormMain> logger, IOrderLogic orderLogic)
        {
            InitializeComponent();
            _logger = logger;
            _orderLogic = orderLogic;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            _logger.LogInformation("Загрузка заказов");
            try
            {
                var list = _orderLogic.ReadList(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns["PastryId"].Visible = false;
                    dataGridView.Columns["PastryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки изделий");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ингредиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormIngredients));
            if (service is FormIngredients form)
            {
                form.ShowDialog();
            }
        }

        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormPastries));
            if (service is FormPastries form)
            {
                form.ShowDialog();
            }
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormCreateOrder));
            if (service is FormCreateOrder form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'В работе'", id);
                try
                {
                    var operationResult = _orderLogic.TakeOrderInWork(new OrderBindingModel { Id = id });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка передачи заказа в работу");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'Готов'", id);
                try
                {
                    var operationResult = _orderLogic.FinishOrder(new OrderBindingModel { Id = id });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка отметки о готовности заказа");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonIssuedOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
                _logger.LogInformation("Заказ №{id}. Меняется статус на 'Выдан'", id);
                try
                {
                    var operationResult = _orderLogic.DeliveryOrder(new OrderBindingModel { Id = id });
                    if (!operationResult)
                    {
                        throw new Exception("Ошибка при сохранении. Дополнительная информация в логах.");
                    }
                    _logger.LogInformation("Заказ №{id} выдан", id);
                    LoadData();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка отметки о выдачи заказа");
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void магазиныToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormShops));
            if (service is FormShops form)
            {
                form.ShowDialog();
            }
        }

        private void пополнениеМагазинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormDelivery));
            if (service is FormDelivery form)
            {
                form.ShowDialog();
            }
        }

        private void списаниеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = Program.ServiceProvider?.GetService(typeof(FormSell));
            if (service is FormSell form)
            {
                form.ShowDialog();
            }
        }
    }
}
