using ConfectioneryContracts.Attributes;
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
        [Column(title:"Номер")]
        public int Id { get; set; }
        [Column(visible:false)]
        public int PastryId { get; set; }
        [Column(title:"Кондитерское изделие", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string PastryName { get; set; } = string.Empty;
        [Column(visible:false)]
        public int? ImplementerId { get; set; }
        [Column(title:"Исполнитель", gridViewAutoSize: GridViewAutoSize.Fill, isUseAutoSize: true)]
        public string? ImplementerFIO { get; set; }
        [Column(visible:false)]
        public int ClientId { get; set; }
        [Column(title:"ФИО клиента", gridViewAutoSize: GridViewAutoSize.Fill, isUseAutoSize: true)]
        public string ClientFIO { get; set; } = string.Empty;
        [Column(visible:false)]
        public string ClientEmail { get; set; } = string.Empty;
        [Column(title:"Количество", width:100)]
        public int Count { get; set; }
        [Column(title:"Сумма", width:100)]
        public double Sum { get; set; }
        [Column(title:"Статус", width:125)]
        public OrderStatus Status { get; set; } = OrderStatus.Неизвестен;
        [Column(title:"Дата создания", width: 120)]
        public DateTime DateCreate { get; set; } = DateTime.Now;
        [Column(title:"Дата выполнения", width: 120)]
        public DateTime? DateImplement { get; set; }
    }
}
