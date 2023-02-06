using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Models
{
    public class Ingredient : IIngredientModel
    {
        public int Id { get; private set; }
        public string IngredientName { get; private set; } = string.Empty;
        public double Cost { get; set; }
        public static Ingredient? Create(IngredientBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Ingredient()
            {
                Id = model.Id,
                IngredientName = model.IngredientName,
                Cost = model.Cost
            };
        }
        public void Update(IngredientBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            IngredientName = model.IngredientName;
            Cost = model.Cost;
        }
        public IngredientViewModel GetViewModel => new()
        {
            Id = Id,
            IngredientName = IngredientName,
            Cost = Cost
        };
    }
}
