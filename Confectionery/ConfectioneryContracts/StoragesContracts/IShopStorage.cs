using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.StoragesContracts
{
    public interface IShopStorage
    {
        List<ShopViewModel> GetFullList();
        List<ShopViewModel> GetFilteredList(ShopSearchModel model);
        ShopViewModel? GetElement(ShopSearchModel model);
        ShopViewModel? Insert(ShopBindingModel model);
        ShopViewModel? Update(ShopBindingModel model);
        ShopViewModel? Delete(ShopBindingModel model);
    }
}
