using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Enums;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    public class Order : IOrderModel
    {
        public int Id { get; private set; }
        [Required]
        public int PastryId { get; private set; }
        [Required]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = new();
        [Required]
        public int Count { get; private set; }
        [Required]
        public double Sum { get; private set; }
        [Required]
        public OrderStatus Status { get; private set; }
        [Required]
        public DateTime DateCreate { get; private set; }
        public DateTime? DateImplement { get; private set; }
        public static Order? Create(ConfectioneryDatabase context, OrderBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Order()
            {
                Id = model.Id,
                PastryId = model.PastryId,
                ClientId = model.ClientId,
                Client = context.Clients.First(x => x.Id == model.ClientId),
                Count = model.Count,
                Sum = model.Sum,
                Status = model.Status,
                DateCreate = model.DateCreate,
                DateImplement = model.DateImplement
            };
        }
        public void Update(OrderBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
            if (model.DateImplement.HasValue) DateImplement = model.DateImplement;
        }
        public OrderViewModel GetViewModel => new()
        {
            Id = Id,
            PastryId = PastryId,
            ClientId = ClientId,
            ClientFIO = Client.ClientFIO,
            Count = Count,
            Sum = Sum,
            Status = Status,
            DateCreate = DateCreate,
            DateImplement = DateImplement
        };
    }
}
