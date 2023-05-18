using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Implements
{
    public class ShopStorage : IShopStorage
    {
        public List<ShopViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Shops
            .Include(x => x.Pastries)
            .ThenInclude(x => x.Pastry)
            .ToList()
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<ShopViewModel> GetFilteredList(ShopSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ShopName))
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return context.Shops
            .Include(x => x.Pastries)
            .ThenInclude(x => x.Pastry)
            .Where(x => x.ShopName.Contains(model.ShopName))
            .ToList()
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public ShopViewModel? GetElement(ShopSearchModel model)
        {
            if (string.IsNullOrEmpty(model.ShopName) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Shops
            .Include(x => x.Pastries)
            .ThenInclude(x => x.Pastry)
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.ShopName) && x.ShopName == model.ShopName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }
        public ShopViewModel? Insert(ShopBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var newShop = Shop.Create(model, context);
            if (newShop == null)
            {
                return null;
            }
            context.Shops.Add(newShop);
            context.SaveChanges();
            return newShop.GetViewModel;
        }
        public ShopViewModel? Update(ShopBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var product = context.Shops.FirstOrDefault(rec => rec.Id == model.Id);
                if (product == null)
                {
                    return null;
                }
                product.Update(model);
                context.SaveChanges();
                product.UpdatePastries(context, model);
                transaction.Commit();
                return product.GetViewModel;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public ShopViewModel? Delete(ShopBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Shops
            .Include(x => x.Pastries)
            .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Shops.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }

        public bool Supply(int pastryId, int count)
        {
            using ConfectioneryDatabase context = new ConfectioneryDatabase();
            var transaction = context.Database.BeginTransaction();
            foreach (Shop shop in context.Shops)
            {
                int freeShopSpace = shop.PastryCapacity - shop.ShopPastries.Select(y => y.Value.Item2).Sum();
                if (freeShopSpace > 0)
                {
                    if (freeShopSpace >= count)
                    {
                        if (shop.ShopPastries.ContainsKey(pastryId))
                        {
                            var shopPastry = shop.ShopPastries[pastryId];
                            shopPastry.Item2 += count;
                            shop.ShopPastries[pastryId] = shopPastry;
                        }
                        else
                        {
                            Pastry pastry = context.Pastrys.First(x => x.Id == pastryId);
                            shop.ShopPastries.Add(pastryId, (pastry, count));
                        }
                        shop.RemapPastries(context);
                        count = 0;
                    }
                    else
                    {
                        int dif = count - freeShopSpace;
                        count -= freeShopSpace;
                        if (shop.ShopPastries.TryGetValue(pastryId, out var pastryCount))
                        {
                            var shopPastry = shop.ShopPastries[pastryId];
                            shopPastry.Item2 = pastryCount.Item2 + freeShopSpace;
                            shop.ShopPastries[pastryId] = shopPastry;
                        }
                        else
                        {
                            Pastry pastry = context.Pastrys.First(x => x.Id == pastryId);
                            shop.ShopPastries.Add(pastryId, (pastry, freeShopSpace));
                        }
                        shop.RemapPastries(context);
                    }
                }
            }
            if (count == 0)
            {
                transaction.Commit();
                return true;
            }
            transaction.Rollback();
            return false;
        }

        public bool Sell(int pastryId, int count)
        {
            using ConfectioneryDatabase context = new ConfectioneryDatabase();
            var transaction = context.Database.BeginTransaction();
            foreach (Shop shop in context.Shops)
            {
                int shopPastryCount = shop.ShopPastries.Select(x => x.Value).Where(x => x.Item1.Id == pastryId).Sum(x => x.Item2);
                if (count - shopPastryCount >= 0)
                {
                    count -= shopPastryCount;
                    shop.ShopPastries.Remove(pastryId);
                }
                else
                {
                    var shopPastry = shop.ShopPastries[pastryId];
                    shopPastry.Item2 -= count;
                    count = 0;
                    shop.ShopPastries[pastryId] = shopPastry;
                }
                shop.RemapPastries(context);
                if (count == 0)
                {
                    transaction.Commit();
                    return true;
                }
            }
            transaction.Rollback();
            return false;
        }
    }
}
