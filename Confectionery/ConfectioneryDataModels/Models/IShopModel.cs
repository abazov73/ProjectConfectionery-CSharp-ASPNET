using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataModels.Models
{
    public interface IShopModel : IId
    {
        string ShopName { get; }
        string ShopAdress { get; }
        DateTime OpeningDate { get; }
        Dictionary<int, (IPastryModel, int)> ShopPastries { get; }
    }
}
