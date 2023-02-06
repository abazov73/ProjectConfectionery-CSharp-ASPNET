using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IIngredientLogic
    {
        List<IngredientViewModel>? ReadList(IngredientSearchModel? model);
        IngredientViewModel? ReadElement(IngredientSearchModel model);
        bool Create(IngredientBindingModel model);
        bool Update(IngredientBindingModel model);
        bool Delete(IngredientBindingModel model);
    }
}
