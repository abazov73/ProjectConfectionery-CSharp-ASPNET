using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BindingModels
{
    public class PastryBindingModel : IPastryModel
    {
        public int Id { get; set; }
        public string PastryName { get; set; } = string.Empty;
        public double Price { get; set; }
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients
        {
            get;
            set;
        } = new();
    }
}
