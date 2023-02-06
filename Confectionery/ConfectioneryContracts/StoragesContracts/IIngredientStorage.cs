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
    public interface IIngredientStorage
    {
        List<IngredientViewModel> GetFullList();
        List<IngredientViewModel> GetFilteredList(IngredientSearchModel model);
        IngredientViewModel? GetElement(IngredientSearchModel model);
        IngredientViewModel? Insert(IngredientBindingModel model);
        IngredientViewModel? Update(IngredientBindingModel model);
        IngredientViewModel? Delete(IngredientBindingModel model);

    }
}
