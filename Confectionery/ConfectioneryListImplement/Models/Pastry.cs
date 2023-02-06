using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Models
{
    public class Pastry : IPastryModel
    {
        public int Id { get; private set; }
        public string PastryName { get; private set; } = string.Empty;
        public double Price { get; private set; }
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients { get; private set; } = new Dictionary<int, (IIngredientModel, int)>();
        public static Pastry? Create(PastryBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Pastry()
            {
                Id = model.Id,
                PastryName = model.PastryName,
                Price = model.Price,
                PastryIngredients = model.PastryIngredients
            };
        }
        public void Update(PastryBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            PastryName = model.PastryName;
            Price = model.Price;
            PastryIngredients = model.PastryIngredients;
        }
        public PastryViewModel GetViewModel => new()
        {
            Id = Id,
            PastryName = PastryName,
            Price = Price,
            PastryIngredients = PastryIngredients
        };
    }
}
