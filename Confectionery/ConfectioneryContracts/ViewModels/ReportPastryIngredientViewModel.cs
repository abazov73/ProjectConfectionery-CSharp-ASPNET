using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class ReportPastryIngredientViewModel
    {
        public string PastryName { get; set; } = string.Empty;
        public int TotalCount { get; set; }
        public List<(string Ingredient, int Count)> Ingredients { get; set; } = new();
    }
}
