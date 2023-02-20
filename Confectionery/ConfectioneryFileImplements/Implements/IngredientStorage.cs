using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        private readonly DataFileSingleton source;
        public IngredientStorage()
        {
            source = DataFileSingleton.GetInstance();
        }
        public List<IngredientViewModel> GetFullList()
        {
            return source.Ingredients
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<IngredientViewModel> GetFilteredList(IngredientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.IngredientName))
            {
                return new();
            }
            return source.Ingredients
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
            return source.Ingredients
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.IngredientName) && x.IngredientName == model.IngredientName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }

        public IngredientViewModel? Insert(IngredientBindingModel model)
        {
            model.Id = source.Ingredients.Count > 0 ? source.Ingredients.Max(x => x.Id) + 1 : 1;
            var newIngredient = Ingredient.Create(model);
            if (newIngredient == null)
            {
                return null;
            }
            source.Ingredients.Add(newIngredient);
            source.SaveIngredients();
            return newIngredient.GetViewModel;
        }
        public IngredientViewModel? Update(IngredientBindingModel model)
        {
            var component = source.Ingredients.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            source.SaveIngredients();
            return component.GetViewModel;
        }
        public IngredientViewModel? Delete(IngredientBindingModel model)
        {
            var element = source.Ingredients.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                source.Ingredients.Remove(element);
                source.SaveIngredients();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
