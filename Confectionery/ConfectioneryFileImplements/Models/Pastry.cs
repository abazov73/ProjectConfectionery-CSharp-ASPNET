using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement.Models
{
    [DataContract]
    public class Pastry
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string PastryName { get; private set; } = string.Empty;
        [DataMember]
        public double Price { get; private set; }
        [DataMember]
        public Dictionary<int, int> Ingredients { get; private set; } = new();
        private Dictionary<int, (IIngredientModel, int)>? _pastryIngredients = null;
        public Dictionary<int, (IIngredientModel, int)> PastryIngredients
        {
            get
            {
                if (_pastryIngredients == null)
                {
                    var source = DataFileSingleton.GetInstance();
                    _pastryIngredients = Ingredients.ToDictionary(x => x.Key, y => ((source.Ingredients.FirstOrDefault(z => z.Id == y.Key) as IIngredientModel)!, y.Value));
                }
                return _pastryIngredients;
            }
        }
        public static Pastry? Create(PastryBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Pastry()
            {
                Id = model.Id,
                PastryName = model.PastryName,
                Price = model.Price,
                Ingredients = model.PastryIngredients.ToDictionary(x => x.Key, x => x.Value.Item2)
            };
        }

        public static Pastry? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Pastry()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                PastryName = element.Element("PastryName")!.Value,
                Price = Convert.ToDouble(element.Element("Price")!.Value),
                Ingredients = element.Element("PastryIngredients")!.Elements("PastryIngredient").ToDictionary(x => Convert.ToInt32(x.Element("Key")?.Value), x => Convert.ToInt32(x.Element("Value")?.Value))
            };
        }

        public void Update(PastryBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            PastryName = model.PastryName;
            Price = model.Price;
            Ingredients = model.PastryIngredients.ToDictionary(x => x.Key, x => x.Value.Item2);
            _pastryIngredients = null;
        }
        public PastryViewModel GetViewModel => new()
        {
            Id = Id,
            PastryName = PastryName,
            Price = Price,
            PastryIngredients = PastryIngredients
        };
        public XElement GetXElement => new("Pastry",
        new XAttribute("Id", Id),
        new XElement("PastryName", PastryName),
        new XElement("Price", Price.ToString()),
        new XElement("PastryIngredients", Ingredients.Select(x => new XElement("PastryIngredient",
        new XElement("Key", x.Key),
        new XElement("Value", x.Value)))
        .ToArray()));
    }
}
