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
    public class Client : IClientModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ClientFIO { get; set; } = string.Empty;
        [DataMember]
        public string Password { get; set; } = string.Empty;
        [DataMember]
        public string Email { get; set; } = string.Empty;
        public static Client? Create(ClientBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Client()
            {
                Id = model.Id,
                ClientFIO = model.ClientFIO,
                Password = model.Password,
                Email = model.Email
            };
        }
        public static Client? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Client()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                ClientFIO = element.Attribute("ClientFIO")!.Value,
                Password = element.Attribute("Password")!.Value,
                Email = element.Attribute("Email")!.Value
            };
        }
        public void Update(ClientBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            ClientFIO = model.ClientFIO;
            Password = model.Password;
            Email = model.Email;
        }
        public ClientViewModel GetViewModel => new()
        {
            Id = Id,
            ClientFIO = ClientFIO,
            Password = Password,
            Email = Email
        };
        public XElement GetXElement => new("Client",
        new XAttribute("Id", Id),
        new XElement("ClientFIO", ClientFIO),
        new XElement("Password", Password),
        new XElement("Email", Email));
    }
}
