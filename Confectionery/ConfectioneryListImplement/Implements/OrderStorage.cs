using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryListImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        private readonly DataListSingleton _source;
        public OrderStorage()
        {
            _source = DataListSingleton.GetInstance();
        }
        public List<OrderViewModel> GetFullList()
        {
            var result = new List<OrderViewModel>();
            foreach (var order in _source.Orders)
            {
                result.Add(AccessPastryStorage(order.GetViewModel));
            }
            return result;
        }

        public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
        {
            var result = new List<OrderViewModel>();
            if (!model.Id.HasValue && !model.DateFrom.HasValue && !model.DateTo.HasValue && !model.OrderStatus.HasValue)
            {
                return result;
            }
            if (model.ImplementerId.HasValue && model.OrderStatus.HasValue)
            {
                foreach (var order in _source.Orders)
                {
                    if (order.Status == model.OrderStatus)
                    {
                        result.Add(AccessPastryStorage(order.GetViewModel));
                    }
                }
            }
            else if (!model.DateFrom.HasValue || !model.DateTo.HasValue)
            {
                foreach (var order in _source.Orders)
                {
                    if (order.Id == model.Id)
                    {
                        result.Add(AccessPastryStorage(order.GetViewModel));
                    }
                }
            }
            else
            {
                foreach (var order in _source.Orders)
                {
                    if (order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo)
                    {
                        result.Add(AccessPastryStorage(order.GetViewModel));
                    }
                }
            }
            return result;
        }

        public OrderViewModel? GetElement(OrderSearchModel model)
        {
            if (!model.Id.HasValue && !model.ImplementerId.HasValue && !model.OrderStatus.HasValue)
            {
                return null;
            }
            if (!model.Id.HasValue)
            {
                foreach (var order in _source.Orders)
                {
                    if (model.ImplementerId.HasValue && model.OrderStatus.HasValue 
                        && order.ImplementerId == model.ImplementerId && order.Status == model.OrderStatus)
                    {
                        return AccessPastryStorage(order.GetViewModel);
                    }
                }
            }
            else
            {
                foreach (var order in _source.Orders)
                {
                    if (model.Id.HasValue && order.Id == model.Id)
                    {
                        return AccessPastryStorage(order.GetViewModel);
                    }
                }
            }
            return null;
        }

        public OrderViewModel? Insert(OrderBindingModel model)
        {
            model.Id = 1;
            foreach (var order in _source.Orders)
            {
                if (model.Id <= order.Id)
                {
                    model.Id = order.Id + 1;
                }
            }
            var newOrder = Order.Create(model);
            if (newOrder == null)
            {
                return null;
            }
            _source.Orders.Add(newOrder);
            return AccessPastryStorage(newOrder.GetViewModel);
        }

        public OrderViewModel? Update(OrderBindingModel model)
        {
            foreach (var order in _source.Orders)
            {
                if (order.Id == model.Id)
                {
                    order.Update(model);
                    return AccessPastryStorage(order.GetViewModel);
                }
            }
            return null;
        }

        public OrderViewModel? Delete(OrderBindingModel model)
        {
            for (int i = 0; i < _source.Orders.Count; ++i)
            {
                if (_source.Orders[i].Id == model.Id)
                {
                    var element = _source.Orders[i];
                    _source.Orders.RemoveAt(i);
                    return AccessPastryStorage(element.GetViewModel);
                }
            }
            return null;
        }

        public OrderViewModel AccessPastryStorage(OrderViewModel model)
        {
            foreach (var pastry in _source.Pastries)
            {
                if (pastry.Id == model.PastryId)
                {
                    model.PastryName = pastry.PastryName;
                    break;
                }
            }
            return model;
        }
    }
}
