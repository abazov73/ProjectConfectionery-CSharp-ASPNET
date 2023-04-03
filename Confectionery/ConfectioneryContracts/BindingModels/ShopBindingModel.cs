using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BindingModels
{
    public class ShopBindingModel : IShopModel
    {
        public int Id { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string ShopAdress { get; set; } = string.Empty;
        public DateTime OpeningDate { get; set; }
        public Dictionary<int, (IPastryModel, int)> ShopPastries
        {
            get;
            set;
        } = new();
    }
}
