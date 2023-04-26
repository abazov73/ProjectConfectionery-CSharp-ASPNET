using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class ShopViewModel : IShopModel
    {
        public int Id { get; set; }
        [DisplayName("Название магазина")]
        public string ShopName { get; set; } = String.Empty;
        [DisplayName("Адресс магазина")]
        public string ShopAdress { get; set; } = String.Empty;
        [DisplayName("Дата открытия")]
        public DateTime OpeningDate { get; set; }
        [DisplayName("Макс. вместимость")]
        public int PastryCapacity { get; set; }
        public Dictionary<int, (IPastryModel, int)> ShopPastries
        {
            get;
            set;
        } = new();
    }
}
