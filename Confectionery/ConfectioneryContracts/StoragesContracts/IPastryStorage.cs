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
    public interface IPastryStorage
    {
        List<PastryViewModel> GetFullList();
        List<PastryViewModel> GetFilteredList(PastrySearchModel model);
        PastryViewModel? GetElement(PastrySearchModel model);
        PastryViewModel? Insert(PastryBindingModel model);
        PastryViewModel? Update(PastryBindingModel model);
        PastryViewModel? Delete(PastryBindingModel model);

    }
}
