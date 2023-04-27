using ConfectioneryDataModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.SearchModels
{
    public class OrderSearchModel
    {
        public int? Id { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? ClientId { get; set; }
        public int? ImplementerId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
    }
}
