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
    public interface IPastryLogic
    {
        List<PastryViewModel>? ReadList(PastrySearchModel? model);
        PastryViewModel? ReadElement(PastrySearchModel model);
        bool Create(PastryBindingModel model);
        bool Update(PastryBindingModel model);
        bool Delete(PastryBindingModel model);
    }
}
