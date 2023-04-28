using ConfectioneryDataModels.Enums;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class OrderViewModel : IOrderModel
    {
        [DisplayName("Номер")]
        public int Id { get; set; }
        public int PastryId { get; set; }
        [DisplayName("Кондитерское изделие")]
        public string PastryName { get; set; } = string.Empty;
        public int? ImplementerId { get; set; }
        [DisplayName("Исполнитель")]
        public string? ImplementerFIO { get; set; }
        public int ClientId { get; set; }
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        [DisplayName("Количество")]
        public int Count { get; set; }
        [DisplayName("Сумма")]
        public double Sum { get; set; }
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; } = OrderStatus.Неизвестен;
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
