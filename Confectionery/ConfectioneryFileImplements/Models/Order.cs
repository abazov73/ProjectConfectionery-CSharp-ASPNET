﻿using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Enums;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement.Models
{
    [DataContract]
    public class Order : IOrderModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public int PastryId { get; private set; }
        [DataMember]
        public int ClientId { get; private set; }
        [DataMember]
        public int? ImplementerId { get; private set; }
        [DataMember]
        public int Count { get; private set; }
        [DataMember]
        public double Sum { get; private set; }
        [DataMember]
        public OrderStatus Status { get; private set; }
        [DataMember]
        public DateTime DateCreate { get; private set; }
        [DataMember]
        public DateTime? DateImplement { get; private set; }

        public static Order? Create(OrderBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Order()
            {
                Id = model.Id,
                PastryId = model.PastryId,
                Count = model.Count,
                Sum = model.Sum,
                Status = model.Status,
                DateCreate = model.DateCreate,
                DateImplement = model.DateImplement,
                ImplementerId = model.ImplementerId
            };
        }

        public static Order? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Order()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                PastryId = Convert.ToInt32(element.Element("PastryId")!.Value),
                Count = Convert.ToInt32(element.Element("Count")!.Value),
                Sum = Convert.ToDouble(element.Element("Sum")!.Value),
                Status = (OrderStatus) Convert.ToInt32(element.Element("Status")!.Value),
                DateCreate = Convert.ToDateTime(element.Element("DateCreate")!.Value),
                DateImplement = Convert.ToDateTime(String.IsNullOrEmpty(element.Element("DateImplement")!.Value) ? null : element.Element("DateImplement")!.Value),
                ImplementerId = Convert.ToInt32(element.Element("ImplementerId")!.Value),
            };
        }

        public void Update(OrderBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            Status = model.Status;
            ImplementerId = model.ImplementerId;
            if (model.DateImplement.HasValue) DateImplement = model.DateImplement;
        }
        public OrderViewModel GetViewModel => new()
        {
            Id = Id,
            PastryId = PastryId,
            Count = Count,
            Sum = Sum,
            Status = Status,
            DateCreate = DateCreate,
            DateImplement = DateImplement,
            ImplementerId = ImplementerId
        };
        public XElement GetXElement => new("Order",
        new XAttribute("Id", Id),
        new XElement("PastryId", PastryId),
        new XElement("Count", Count.ToString()),
        new XElement("Sum", Sum.ToString()),
        new XElement("Status", ((int)Status).ToString()),
        new XElement("DateCreate", DateCreate.ToString()),
        new XElement("ImplementerId", ImplementerId.ToString()),
        new XElement("DateImplement", DateImplement.ToString()));
    }
}
