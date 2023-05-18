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
    public class Shop : IShopModel
    {
        public int Id {  get; private set; }
        [Required]
        public string ShopName {  get; private set; } = string.Empty;
        [Required]
        public string ShopAdress {  get; private set; } = string.Empty;
        [Required]
        public DateTime OpeningDate { get; private set; }
        [Required]
        public int PastryCapacity { get; private set; }
        private Dictionary<int, (IPastryModel, int)>? _shopPastries = null;
        [NotMapped]
        public Dictionary<int, (IPastryModel, int)> ShopPastries
        {
            get
            {
                if (_shopPastries == null)
                {
                    _shopPastries = Pastries
                    .ToDictionary(recSP => recSP.PastryId, recPI => (recPI.Pastry as IPastryModel, recPI.Count));
                }
                return _shopPastries;
            }
        }
        [ForeignKey("ShopId")]
        public virtual List<ShopPastry> Pastries { get; set; } = new();

        public static Shop Create(ShopBindingModel model, ConfectioneryDatabase context)
        {
            return new Shop()
            {
                Id = model.Id,
                ShopAdress = model.ShopAdress,
                ShopName = model.ShopName,
                OpeningDate = model.OpeningDate,
                PastryCapacity = model.PastryCapacity,
                Pastries = model.ShopPastries.Select(x => new ShopPastry {
                    Pastry = context.Pastrys.First(y => y.Id == x.Key), Count = x.Value.Item2 }).ToList()
            };
        }

        public void Update(ShopBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            ShopName = model.ShopName;
            ShopAdress = model.ShopAdress;
            OpeningDate = model.OpeningDate;
            PastryCapacity = model.PastryCapacity;
        }

        public void UpdatePastries(ConfectioneryDatabase context, ShopBindingModel model)
        {
            var list = context.ShopPastries.ToList();
            var shopPastries = context.ShopPastries.Where(rec => rec.ShopId == model.Id).ToList();
            if (shopPastries != null)
            {
                // удалили те, которых нет в модели
                context.ShopPastries.RemoveRange(shopPastries.Where(rec => !model.ShopPastries.ContainsKey(rec.PastryId)));
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateIngredient in shopPastries)
                {
                    updateIngredient.Count = model.ShopPastries[updateIngredient.PastryId].Item2;
                    model.ShopPastries.Remove(updateIngredient.PastryId);
                }
                var shop = context.Shops.First(x => x.Id == Id);
                foreach (var sp in model.ShopPastries)
                {
                    context.ShopPastries.Add(new ShopPastry
                    {
                        Shop = shop,
                        Pastry = context.Pastrys.First(x => x.Id == sp.Key),
                        Count = sp.Value.Item2
                    });
                    context.SaveChanges();
                }
                _shopPastries = null;
            }
        }

        public ShopViewModel GetViewModel => new()
        {
            Id = Id,
            ShopName = ShopName,
            ShopAdress = ShopAdress,
            ShopPastries = ShopPastries,
            OpeningDate = OpeningDate,
            PastryCapacity = PastryCapacity,
        };

        public void RemapPastries(ConfectioneryDatabase context)
        {
            UpdatePastries(context, new ShopBindingModel { Id = Id, ShopPastries = ShopPastries });
        }
    }
}
