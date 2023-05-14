using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    public class Pastry : IPastryModel
    {
        public int Id { get; set; }
        [Required]
        public string PastryName { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        private Dictionary<int, (IIngredientModel, int)>? _pastryIngredients = null;
        [NotMapped]
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients
        {
            get
            {
                if (_pastryIngredients == null)
                {
                    _pastryIngredients = Ingredients
                    .ToDictionary(recPI => recPI.IngredientId, recPI => (recPI.Ingredient as IIngredientModel, recPI.Count));
                }
                return _pastryIngredients;
            }
        }
        [ForeignKey("PastryId")]
        public virtual List<PastryIngredient> Ingredients { get; set; } = new();
        [ForeignKey("PastryId")]
        public virtual List<Order> Orders { get; set; } = new();
        public static Pastry Create(ConfectioneryDatabase context, PastryBindingModel model)
        {
            return new Pastry()
            {
                Id = model.Id,
                PastryName = model.PastryName,
                Price = model.Price,
                Ingredients = model.PastryIngredients.Select(x => new PastryIngredient
                {
                    Ingredient = context.Ingredients.First(y => y.Id == x.Key),
                    Count = x.Value.Item2
                }).ToList()
            };
        }
        public void Update(PastryBindingModel model)
        {
            PastryName = model.PastryName;
            Price = model.Price;
        }
        public PastryViewModel GetViewModel => new()
        {
            Id = Id,
            PastryName = PastryName,
            Price = Price,
            PastryIngredients = PastryIngredients
        };
        public void UpdateIngredients(ConfectioneryDatabase context, PastryBindingModel model)
        {
            var pastryIngredients = context.PastryIngredients.Where(rec => rec.PastryId == model.Id).ToList();
            if (pastryIngredients != null && pastryIngredients.Count > 0)
            {
                // удалили те, которых нет в модели
                context.PastryIngredients.RemoveRange(pastryIngredients.Where(rec => !model.PastryIngredients.ContainsKey(rec.IngredientId)));
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in pastryIngredients)
                {
                    updateIngredient.Count = model.PastryIngredients[updateIngredient.IngredientId].Item2;
                    model.PastryIngredients.Remove(updateIngredient.IngredientId);
                }
                var pastry = context.Pastrys.First(x => x.Id == Id);
                foreach (var pc in model.PastryIngredients)
                {
                    context.PastryIngredients.Add(new PastryIngredient
                    {
                        Pastry = pastry,
                        Ingredient = context.Ingredients.First(x => x.Id == pc.Key),
                        Count = pc.Value.Item2
                    });
                    context.SaveChanges();
                }
                _pastryIngredients = null;
            }
        }
    }
}
