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
    public class PastryViewModel : IPastryModel
    {
        [Column(visible:false)]
        public int Id { get; set; }
        [Column(title:"Название кондитерского изделия", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string PastryName { get; set; } = string.Empty;
        [Column(title:"Цена", width:75)]
        public double Price { get; set; }
        [Column(visible:false)]
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients
        {
            get;
            set;
        } = new();
    }
}
