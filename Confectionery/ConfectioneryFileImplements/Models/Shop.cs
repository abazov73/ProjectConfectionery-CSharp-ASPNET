using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement.Models
{
    public class Shop : IShopModel
    {
        public int Id { get; private set; }
        public string ShopName { get; private set; } = string.Empty;
        public string ShopAdress { get; private set; } = string.Empty;
        public int PastryCapacity { get; private set; }
        public DateTime OpeningDate { get; private set; }
        public Dictionary<int, int> Pastries { get; private set; } = new();
        private Dictionary<int, (IPastryModel, int)>? _shopPastries = null;
        public Dictionary<int, (IPastryModel, int)> ShopPastries 
        { 
            get
            {
                if (_shopPastries == null)
                {
                    var source = DataFileSingleton.GetInstance();
                    _shopPastries = Pastries.ToDictionary(x => x.Key, y => ((source.Pastries.FirstOrDefault(z => z.Id == y.Key) as IPastryModel)!, y.Value));
                }
                return _shopPastries;
            }
        }
        public static Shop? Create(ShopBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Shop()
            {
                Id = model.Id,
                ShopName = model.ShopName,
                ShopAdress = model.ShopAdress,
                OpeningDate = model.OpeningDate,
                Pastries = model.ShopPastries.ToDictionary(x => x.Key, x => x.Value.Item2),
                PastryCapacity = model.PastryCapacity
            };
        }
        public static Shop? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Shop()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                ShopName = element.Element("ShopName")!.Value,
                ShopAdress = element.Element("ShopAdress")!.Value,
                PastryCapacity = Convert.ToInt32(element.Element("PastryCapacity")!.Value),
                OpeningDate = Convert.ToDateTime(element.Element("OpeningDate")!.Value),
                Pastries = element.Element("ShopPastries")!.Elements("ShopPastry").ToDictionary(x => Convert.ToInt32(x.Element("Key")?.Value), x => Convert.ToInt32(x.Element("Value")?.Value))
            };
        }
        public void Update(ShopBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            ShopName = model.ShopName;
            ShopAdress = model.ShopAdress;
            OpeningDate = model.OpeningDate;
            Pastries = model.ShopPastries.ToDictionary(x => x.Key, x => x.Value.Item2);
            _shopPastries = null;
            PastryCapacity = model.PastryCapacity;
        }
        public ShopViewModel GetViewModel => new()
        {
            Id = Id,
            ShopName = ShopName,
            ShopAdress = ShopAdress,
            ShopPastries = ShopPastries,
            OpeningDate = OpeningDate,
            PastryCapacity = PastryCapacity,
        };
        internal void setShopPastriesNull()
        {
            _shopPastries = null;
        }
        public XElement GetXElement => new("Shop",
        new XAttribute("Id", Id),
        new XElement("ShopName", ShopName),
        new XElement("ShopAdress", ShopAdress),
        new XElement("PastryCapacity", PastryCapacity.ToString()),
        new XElement("OpeningDate", OpeningDate.ToString()),
        new XElement("ShopPastries", Pastries.Select(x => new XElement("ShopPastry",
        new XElement("Key", x.Key),
        new XElement("Value", x.Value)))
        .ToArray()));
    }
}
