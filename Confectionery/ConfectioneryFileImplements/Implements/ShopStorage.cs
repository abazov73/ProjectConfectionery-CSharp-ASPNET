using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class ShopStorage : IShopStorage
    {
        private readonly DataFileSingleton source;
        public ShopStorage()
        {
            source = DataFileSingleton.GetInstance();
        }
        public List<ShopViewModel> GetFullList()
        {
            return source.Shops
            .Select(x => x.GetViewModel)
            .ToList();
        }

        public List<ShopViewModel> GetFilteredList(ShopSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ShopName))
            {
                return new();
            }
            return source.Shops
            .Where(x => x.ShopName.Contains(model.ShopName))
            .Select(x => x.GetViewModel)
            .ToList();
        }

        public ShopViewModel? GetElement(ShopSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ShopName) && !model.Id.HasValue)
            {
                return null;
            }
            return source.Shops
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.ShopName) && x.ShopName == model.ShopName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }

        public ShopViewModel? Insert(ShopBindingModel model)
        {
            model.Id = source.Shops.Count > 0 ? source.Shops.Max(x => x.Id) + 1 : 1;
            var newShop = Shop.Create(model);
            if (newShop == null)
            {
                return null;
            }
            source.Shops.Add(newShop);
            source.SaveShops();
            return newShop.GetViewModel;
        }

        public ShopViewModel? Update(ShopBindingModel model)
        {
            var pastry = source.Shops.FirstOrDefault(x => x.Id == model.Id);
            if (pastry == null)
            {
                return null;
            }
            pastry.Update(model);
            source.SaveShops();
            return pastry.GetViewModel;
        }

        public ShopViewModel? Delete(ShopBindingModel model)
        {
            var element = source.Shops.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                source.Shops.Remove(element);
                source.SaveShops();
                return element.GetViewModel;
            }
            return null;
        }

        public bool Supply(int pastryId, int count)
        {
            int freeSpace = source.Shops.Select(x => x.PastryCapacity - x.ShopPastries.Select(y => y.Value.Item2).Sum()).Sum();
            if (freeSpace < count)
            {
                return false;
            }
            foreach (Shop shop in source.Shops){
                int freeShopSpace = shop.PastryCapacity - shop.ShopPastries.Select(y => y.Value.Item2).Sum();
                if (freeShopSpace > 0)
                {
                    if (freeShopSpace >= count)
                    {
                        if (shop.Pastries.ContainsKey(pastryId))
                        {
                            shop.Pastries[pastryId] += count;
                        }
                        else
                        {
                            shop.Pastries.Add(pastryId, count);
                        }
                        shop.setShopPastriesNull();
                        return true;
                    }
                    else
                    {
                        count -= freeShopSpace;
                        if (shop.Pastries.TryGetValue(pastryId, out var pastryCount))
                        {
                            shop.Pastries[pastryId] = pastryCount + freeShopSpace;
                        }
                        else
                        {
                            shop.Pastries.Add(pastryId, freeShopSpace);
                        }
                        shop.setShopPastriesNull();
                    }
                }
            }
            return false;
        }

        public bool Sell(int pastryId, int count)
        {
            int pastryCount = source.Shops.Select(x => x.ShopPastries.Select(y => y.Value).Where(y => y.Item1.Id == pastryId).Sum(y => y.Item2)).Sum();
            if (pastryCount < count)
            {
                return false;
            }
            foreach (Shop shop in source.Shops)
            {
                int shopPastryCount = shop.ShopPastries.Select(x => x.Value).Where(x => x.Item1.Id == pastryId).Sum(x => x.Item2);
                if (count - shopPastryCount >= 0)
                {
                    count -= shopPastryCount;
                    shop.Pastries.Remove(pastryId);
                    shop.setShopPastriesNull();
                }
                else
                {
                    shop.Pastries[pastryId] -= count;
                    count = 0;
                    shop.setShopPastriesNull();
                }
                if (count == 0)
                {
                    source.SaveShops();
                    return true;
                }
            }
            return false;
        }
    }
}
