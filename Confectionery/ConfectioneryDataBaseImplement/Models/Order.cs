using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Enums;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    [DataContract]
    public class Order : IOrderModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        [Required]
        public int PastryId { get; private set; }
        [DataMember]
        [Required]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; } = new();
        [DataMember]
        public int? ImplementerId { get; set; }
        public virtual Implementer? Implementer { get; set; } = new();
        [DataMember]
        [Required]
        public int Count { get; private set; }
        [DataMember]
        [Required]
        public double Sum { get; private set; }
        [DataMember]
        [Required]
        public OrderStatus Status { get; private set; }
        [DataMember]
        [Required]
        public DateTime DateCreate { get; private set; }
        [DataMember]
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
                DateImplement = model.DateImplement,
                ImplementerId = model.ImplementerId,
                Implementer = model.ImplementerId.HasValue ? context.Implementers.First(x => x.Id == model.ImplementerId) : null,
            };
        }
        public void Update(ConfectioneryDatabase context, OrderBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
            if (model.ImplementerId.HasValue) ImplementerId = model.ImplementerId;
            if (model.ImplementerId.HasValue) Implementer = context.Implementers.First(x => x.Id == model.ImplementerId);
            if (model.DateImplement.HasValue) DateImplement = model.DateImplement;
        }
        public OrderViewModel GetViewModel => new()
        {
            Id = Id,
            PastryId = PastryId,
            ClientId = ClientId,
            ClientFIO = Client.ClientFIO,
            ClientEmail = Client.Email,
            Count = Count,
            Sum = Sum,
            Status = Status,
            DateCreate = DateCreate,
            DateImplement = DateImplement,
            ImplementerId = ImplementerId,
            ImplementerFIO = Implementer?.ImplementerFIO
        };
    }
}
