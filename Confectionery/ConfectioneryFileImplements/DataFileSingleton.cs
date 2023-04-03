using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement
{
    public class DataFileSingleton
    {
        private static DataFileSingleton? instance;
        private readonly string IngredientFileName = "Ingredient.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PastryFileName = "Pastry.xml";
        private readonly string ClientFileName = "Client.xml";
        public List<Ingredient> Ingredients { get; private set; }
        public List<Order> Orders { get; private set; }
        public List<Pastry> Pastries { get; private set; }
        public List<Client> Clients { get; private set; }
        public static DataFileSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataFileSingleton();
            }
            return instance;
        }
        public void SaveIngredients() => SaveData(Ingredients, IngredientFileName, "Ingredients", x => x.GetXElement);
        public void SavePastries() => SaveData(Pastries, PastryFileName, "Pastries", x => x.GetXElement);
        public void SaveOrders() => SaveData(Orders, OrderFileName, "Orders", x => x.GetXElement);
        public void SaveClients() => SaveData(Clients, ClientFileName, "Clients", x => x.GetXElement);
        private DataFileSingleton()
        {
            Ingredients = LoadData(IngredientFileName, "Ingredient", x => Ingredient.Create(x)!)!;
            Pastries = LoadData(PastryFileName, "Pastry", x => Pastry.Create(x)!)!;
            Orders = LoadData(OrderFileName, "Order", x => Order.Create(x)!)!;
            Clients = LoadData(ClientFileName, "Client", x => Client.Create(x)!)!;
        }
        private static List<T>? LoadData<T>(string filename, string xmlNodeName, Func<XElement, T> selectFunction)
        {
            if (File.Exists(filename))
            {
                return XDocument.Load(filename)?.Root?.Elements(xmlNodeName)?.Select(selectFunction)?.ToList();
            }
            return new List<T>();
        }
        private static void SaveData<T>(List<T> data, string filename, string xmlNodeName, Func<T, XElement> selectFunction)
        {
            if (data != null)
            {
                new XDocument(new XElement(xmlNodeName, data.Select(selectFunction).ToArray())).Save(filename);
            }
        }
    }
}
