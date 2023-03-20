using ConfectioneryDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class ReportOrdersViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string PastryName { get; set; } = string.Empty;
        public double Sum { get; set; }
        public String Status { get; set; } = "Неизвестен";
    }
}
