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
    public interface IImplementerStorage
    {
        List<ImplementerViewModel> GetFullList();
        List<ImplementerViewModel> GetFilteredList(ImplementerSearchModel model);
        ImplementerViewModel? GetElement(ImplementerSearchModel model);
        ImplementerViewModel? Insert(ImplementerBindingModel model);
        ImplementerViewModel? Update(ImplementerBindingModel model);
        ImplementerViewModel? Delete(ImplementerBindingModel model);
    }
}
