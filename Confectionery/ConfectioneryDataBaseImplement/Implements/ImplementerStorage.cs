using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataBaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Implementers
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<ImplementerViewModel> GetFilteredList(ImplementerSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ImplementerFIO) && !model.Id.HasValue)
            {
                return new();
            }
            if (!string.IsNullOrEmpty(model.ImplementerFIO) && !string.IsNullOrEmpty(model.Password))
            {
                using var context = new ConfectioneryDatabase();
                return context.Implementers
                .Where(x => x.ImplementerFIO.Equals(model.ImplementerFIO))
                .Select(x => x.GetViewModel)
                .ToList();
            }
            else
            {
                using var context = new ConfectioneryDatabase();
                return context.Implementers
                .Where(x => x.Id == model.Id)
                .Select(x => x.GetViewModel)
                .ToList();
            }
        }
        public ImplementerViewModel? GetElement(ImplementerSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ImplementerFIO) && string.IsNullOrEmpty(model.Password) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Implementers
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.ImplementerFIO) && !string.IsNullOrEmpty(model.Password)
            && x.ImplementerFIO == model.ImplementerFIO && x.Password == model.Password) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }
        public ImplementerViewModel? Insert(ImplementerBindingModel model)
        {
            var newImplementer = Implementer.Create(model);
            if (newImplementer == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            context.Implementers.Add(newImplementer);
            context.SaveChanges();
            return newImplementer.GetViewModel;
        }
        public ImplementerViewModel? Update(ImplementerBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var component = context.Implementers.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            context.SaveChanges();
            return component.GetViewModel;
        }
        public ImplementerViewModel? Delete(ImplementerBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Implementers.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
