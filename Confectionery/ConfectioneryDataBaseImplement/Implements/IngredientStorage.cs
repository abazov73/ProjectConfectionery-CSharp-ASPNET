using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataBaseImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        public List<IngredientViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Ingredients
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<IngredientViewModel> GetFilteredList(IngredientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.IngredientName))
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return context.Ingredients
            .Where(x => x.IngredientName.Contains(model.IngredientName))
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public IngredientViewModel? GetElement(IngredientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.IngredientName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Ingredients
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.IngredientName) && x.IngredientName == model.IngredientName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }
        public IngredientViewModel? Insert(IngredientBindingModel model)
        {
            var newIngredient = Ingredient.Create(model);
            if (newIngredient == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            context.Ingredients.Add(newIngredient);
            context.SaveChanges();
            return newIngredient.GetViewModel;
        }
        public IngredientViewModel? Update(IngredientBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var component = context.Ingredients.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            context.SaveChanges();
            return component.GetViewModel;
        }
        public IngredientViewModel? Delete(IngredientBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Ingredients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Ingredients.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
