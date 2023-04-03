using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryContracts.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel>? ReadList(ClientSearchModel? model);
        ClientViewModel? ReadElement(ClientSearchModel model);
        bool Create(ClientBindingModel model);
        bool Update(ClientBindingModel model);
        bool Delete(ClientBindingModel model);
    }
}
