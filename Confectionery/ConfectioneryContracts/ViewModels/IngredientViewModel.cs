using ConfectioneryContracts.Attributes;
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
        [Column(visible:false)]
        public int Id { get; set; }
        [Column(title:"Название ингредиента", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string IngredientName { get; set; } = string.Empty;
        [Column(title:"Цена", width:50)]
        public double Cost { get; set; }

    }
}
