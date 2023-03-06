using ConfectioneryContracts.BindingModels;
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
    public class PastryStorage : IPastryStorage
    {
        public List<PastryViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Pastrys
            .Include(x => x.Ingredients)
            .ThenInclude(x => x.Ingredient)
            .ToList()
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<PastryViewModel> GetFilteredList(PastrySearchModel model)
        {
            if (string.IsNullOrEmpty(model.PastryName))
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return context.Pastrys
            .Include(x => x.Ingredients)
            .ThenInclude(x => x.Ingredient)
            .Where(x => x.PastryName.Contains(model.PastryName))
            .ToList()
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public PastryViewModel? GetElement(PastrySearchModel model)
        {
            if (string.IsNullOrEmpty(model.PastryName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Pastrys
            .Include(x => x.Ingredients)
            .ThenInclude(x => x.Ingredient)
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.PastryName) && x.PastryName == model.PastryName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }
        public PastryViewModel? Insert(PastryBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var newPastry = Pastry.Create(context, model);
            if (newPastry == null)
            {
                return null;
            }
            context.Pastrys.Add(newPastry);
            context.SaveChanges();
            return newPastry.GetViewModel;
        }
        public PastryViewModel? Update(PastryBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var product = context.Pastrys.FirstOrDefault(rec => rec.Id == model.Id);
                if (product == null)
                {
                    return null;
                }
                product.Update(model);
                context.SaveChanges();
                product.UpdateIngredients(context, model);
                transaction.Commit();
                return product.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public PastryViewModel? Delete(PastryBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Pastrys
            .Include(x => x.Ingredients)
            .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Pastrys.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
