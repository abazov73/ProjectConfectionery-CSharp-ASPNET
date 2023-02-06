using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class IngredientViewModel : IIngredientModel
    {
        public int Id { get; set; }
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; } = string.Empty;
        [DisplayName("Цена")]
        public double Cost { get; set; }

    }
}
