using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataFileSingleton source;
        public OrderStorage()
        {
            source = DataFileSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            return source.Orders
            .Select(x => AccessPastryStorage(x.GetViewModel))
            .ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            return source.Orders
            .Where(x => x.Id == model.Id)
            .Select(x => AccessPastryStorage(x.GetViewModel))
            .ToList();
        }

        public OrderViewModel? GetElement(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return null;
            }
            return AccessPastryStorage(source.Orders
            .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
            ?.GetViewModel);
        }

        public OrderViewModel? Insert(OrderBindingModel model)
        {
            model.Id = source.Orders.Count > 0 ? source.Orders.Max(x => x.Id) + 1 : 1;
            var newOrder = Order.Create(model);
            if (newOrder == null)
            {
                return null;
            }
            source.Orders.Add(newOrder);
            source.SaveOrders();
            return AccessPastryStorage(newOrder.GetViewModel);
        }

        public OrderViewModel? Update(OrderBindingModel model)
        {
            var order = source.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
            {
                return null;
            }
            order.Update(model);
            source.SaveOrders();
            return AccessPastryStorage(order.GetViewModel);
        }

        public OrderViewModel? Delete(OrderBindingModel model)
        {
            var element = source.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                source.Orders.Remove(element);
                source.SaveOrders();
                return AccessPastryStorage(element.GetViewModel);
            }
            return null;
        }

        public OrderViewModel AccessPastryStorage(OrderViewModel model)
        {
            string? pastryName = source.Pastries.FirstOrDefault(x => x.Id == model?.PastryId)?.PastryName;
            if (pastryName != null) model.PastryName = pastryName;
            return model;
        }
    }
}
