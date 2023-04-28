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
    public partial class FormMails : Form
    {
        private readonly ILogger _logger;
        private readonly IMessageInfoLogic _logic;
        public FormMails(ILogger<FormMails> logger, IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logger = logger;
            _logic = logic;
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            try
            {
                var list = _logic.ReadList(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns["MessageId"].Visible = false;
                    dataGridView.Columns["ClientId"].Visible = false;
                    dataGridView.Columns["Body"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                _logger.LogInformation("Загрузка писем");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка загрузки писем");
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
