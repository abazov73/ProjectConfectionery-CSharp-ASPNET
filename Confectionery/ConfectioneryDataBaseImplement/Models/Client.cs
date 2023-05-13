using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    [DataContract]
    public class Client : IClientModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        [Required]
        public string ClientFIO { get; private set; } = string.Empty;
        [DataMember]
        [Required]
        public string Password { get; set; } = string.Empty;
        [DataMember]
        [Required]
        public string Email { get; private set; } = string.Empty;

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
                Email = model.Email,
            };
        }
        public static Client? Create(ClientViewModel? model)
        {
            return new Client()
            {
                Id = model.Id,
                ClientFIO = model.ClientFIO,
                Password = model.Password,
                Email = model.Email,
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
        }
        public ClientViewModel GetViewModel => new()
        {
            Id = Id,
            ClientFIO = ClientFIO,
            Password = Password,
            Email = Email,
        };
    }
}
