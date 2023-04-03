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
    public interface IClientStorage
    {
        List<ClientViewModel> GetFullList();
        List<ClientViewModel> GetFilteredList(ClientSearchModel model);
        ClientViewModel? GetElement(ClientSearchModel model);
        ClientViewModel? Insert(ClientBindingModel model);
        ClientViewModel? Update(ClientBindingModel model);
        ClientViewModel? Delete(ClientBindingModel model);
    }
}
