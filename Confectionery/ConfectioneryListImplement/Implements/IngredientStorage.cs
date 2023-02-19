using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Implements
{
    public class IngredientStorage : IIngredientStorage
    {
        private readonly DataListSingleton _source;
        public IngredientStorage()
        {
            _source = DataListSingleton.GetInstance();
        }
        public List<IngredientViewModel> GetFullList()
        {
            var result = new List<IngredientViewModel>();
            foreach (var ingredient in _source.Ingredients)
            {
                result.Add(ingredient.GetViewModel);
            }
            return result;
        }

        public List<IngredientViewModel> GetFilteredList(IngredientSearchModel model)
        {
            var result = new List<IngredientViewModel>();
            if (string.IsNullOrEmpty(model.IngredientName))
            {
                return result;
            }
            foreach (var ingredient in _source.Ingredients)
            {
                if (ingredient.IngredientName.Contains(model.IngredientName))
                {
                    result.Add(ingredient.GetViewModel);
                }
            }
            return result;
        }

        public IngredientViewModel? GetElement(IngredientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.IngredientName) && !model.Id.HasValue)
            {
                return null;
            }
            foreach (var ingredient in _source.Ingredients)
            {
                if ((!string.IsNullOrEmpty(model.IngredientName) && ingredient.IngredientName == model.IngredientName) ||
                (model.Id.HasValue && ingredient.Id == model.Id))
                {
                    return ingredient.GetViewModel;
                }
            }
            return null;
        }

        public IngredientViewModel? Insert(IngredientBindingModel model)
        {
            model.Id = 1;
            foreach (var ingredient in _source.Ingredients)
            {
                if (model.Id <= ingredient.Id)
                {
                    model.Id = ingredient.Id + 1;
                }
            }
            var newIngredient = Ingredient.Create(model);
            if (newIngredient == null)
            {
                return null;
            }
            _source.Ingredients.Add(newIngredient);
            return newIngredient.GetViewModel;
        }

        public IngredientViewModel? Update(IngredientBindingModel model)
        {
            foreach (var ingredient in _source.Ingredients)
            {
                if (ingredient.Id == model.Id)
                {
                    ingredient.Update(model);
                    return ingredient.GetViewModel;
                }
            }
            return null;
        }

        public IngredientViewModel? Delete(IngredientBindingModel model)
        {
            for (int i = 0; i < _source.Ingredients.Count; ++i)
            {
                if (_source.Ingredients[i].Id == model.Id)
                {
                    var element = _source.Ingredients[i];
                    _source.Ingredients.RemoveAt(i);
                    return element.GetViewModel;
                }
            }
            return null;
        }
    }
}
