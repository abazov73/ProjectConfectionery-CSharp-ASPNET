using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class PastryViewModel : IPastryModel
    {
        public int Id { get; set; }
        [DisplayName("Название кондитерского изделия")]
        public string PastryName { get; set; } = string.Empty;
        [DisplayName("Цена")]
        public double Price { get; set; }
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients
        {
            get;
            set;
        } = new();
    }
}
