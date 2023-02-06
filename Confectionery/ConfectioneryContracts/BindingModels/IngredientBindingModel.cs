using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectioneryDataModels.Models;

namespace ConfectioneryContracts.BindingModels
{
    public class IngredientBindingModel : IIngredientModel
    {
        public int Id { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public double Cost { get; set; }

    }
}
