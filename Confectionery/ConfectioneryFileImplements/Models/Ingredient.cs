using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement.Models
{
    public class Ingredient : IIngredientModel
    {
        public int Id { get; private set; }
        public string IngredientName { get; private set; } = string.Empty;
        public double Cost { get; set; }
        public static Ingredient? Create(IngredientBindingModel model)
        {
            if (model == null) return null;
            return new Ingredient()
            {
                Id = model.Id,
                IngredientName = model.IngredientName,
                Cost = model.Cost
            };
        }
        public static Ingredient? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Ingredient()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                IngredientName = element.Element("IngredientName")!.Value,
                Cost = Convert.ToDouble(element.Element("Cost")!.Value)
            };
        }
        public void Update(IngredientBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            IngredientName = model.IngredientName;
            Cost = model.Cost;
        }
        public IngredientViewModel GetViewModel => new()
        {
            Id = Id,
            IngredientName = IngredientName,
            Cost = Cost
        };
        public XElement GetXElement => new("Ingredient",
        new XAttribute("Id", Id),
        new XElement("IngredientName", IngredientName),
        new XElement("Cost", Cost.ToString()));
    }
}
