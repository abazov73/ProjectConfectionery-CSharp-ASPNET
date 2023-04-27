using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectioneryDataModels.Enums;

namespace ConfectioneryDataModels.Models
{
    public interface IOrderModel : IId
    {
        int PastryId { get; }
        int ClientId { get; }
        int Count { get; }
        double Sum { get; }
        OrderStatus Status { get; }
        DateTime DateCreate { get; }
        DateTime? DateImplement { get; }
        int? ImplementerId { get; }

    }
}
