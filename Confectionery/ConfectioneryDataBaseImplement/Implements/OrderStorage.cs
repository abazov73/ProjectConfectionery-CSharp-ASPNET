﻿using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Orders.Include(x => x.Client)
            .Select(x => AccessPastryStorage(x.GetViewModel, context))
            .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderSearchModel model)
        {
            if (!model.Id.HasValue && !model.DateFrom.HasValue && !model.DateTo.HasValue && !model.ClientId.HasValue)
            {
                return new();
            }
            if (model.ClientId.HasValue)
            {
                using var context = new ConfectioneryDatabase();
                return context.Orders.Include(x => x.Client)
                .Where(x => x.ClientId == model.ClientId)
                .Select(x => AccessPastryStorage(x.GetViewModel, context))
                .ToList();
            }
            else if (!model.DateFrom.HasValue || !model.DateTo.HasValue)
            {
                using var context = new ConfectioneryDatabase();
                return context.Orders.Include(x => x.Client)
                .Where(x => x.Id == model.Id)
                .Select(x => AccessPastryStorage(x.GetViewModel, context))
                .ToList();
            }
            else
            {
                using var context = new ConfectioneryDatabase();
                return context.Orders.Include(x => x.Client)
                .Where(x => x.DateCreate >= model.DateFrom && x.DateCreate <= model.DateTo)
                .Select(x => AccessPastryStorage(x.GetViewModel, context))
                .ToList();
            }
        }
        public OrderViewModel? GetElement(OrderSearchModel model)
        {
            if (!model.Id.HasValue)
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return AccessPastryStorage(context.Orders.Include(x => x.Client)
            .FirstOrDefault(x => model.Id.HasValue && x.Id == model.Id)
            ?.GetViewModel, context);
        }
        public OrderViewModel? Insert(OrderBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var newOrder = Order.Create(context, model);
            if (newOrder == null)
            {
                return null;
            };
            context.Orders.Add(newOrder);
            context.SaveChanges();
            return AccessPastryStorage(newOrder.GetViewModel, context);
        }
        public OrderViewModel? Update(OrderBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var order = context.Orders.FirstOrDefault(x => x.Id == model.Id);
            if (order == null)
            {
                return null;
            }
            order.Update(model);
            context.SaveChanges();
            return AccessPastryStorage(order.GetViewModel, context);
        }
        public OrderViewModel? Delete(OrderBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
                return AccessPastryStorage(element.GetViewModel, context);
            }
            return null;
        }
        static OrderViewModel AccessPastryStorage(OrderViewModel model, ConfectioneryDatabase context)
        {
            if (model == null) return model;
            string? pastryName = context.Pastrys.FirstOrDefault(x => x.Id == model.PastryId)?.PastryName;
            if (pastryName != null) model.PastryName = pastryName;
            return model;
        }
    }
}
