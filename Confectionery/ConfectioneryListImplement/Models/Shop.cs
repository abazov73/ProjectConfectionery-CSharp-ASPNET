using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Models
{
    public class Shop : IShopModel
    {
        public int Id { get; private set; }
        public string ShopName { get; private set; } = string.Empty;
        public string ShopAdress { get; private set; } = string.Empty;
        public DateTime OpeningDate { get; private set; }
        public Dictionary<int, (IPastryModel, int)> ShopPastries { get; private set; } = new Dictionary<int, (IPastryModel, int)>();
        public static Shop? Create(ShopBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Shop()
            {
                Id = model.Id,
                ShopName = model.ShopName,
                ShopAdress = model.ShopAdress,
                OpeningDate = model.OpeningDate,
                ShopPastries = model.ShopPastries
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
            ShopPastries = model.ShopPastries;
        }
        public ShopViewModel GetViewModel => new()
        {
            Id = Id,
            ShopName = ShopName,
            ShopAdress = ShopAdress,
            ShopPastries = ShopPastries,
            OpeningDate = OpeningDate
        };
    }
}
