using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Models
{
    public class Client : IClientModel
    {
        public int Id { get; set; }
        public string ClientFIO { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
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
    }
}
