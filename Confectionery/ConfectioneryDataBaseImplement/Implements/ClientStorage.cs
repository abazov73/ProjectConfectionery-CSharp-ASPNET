using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataBaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.Clients
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<ClientViewModel> GetFilteredList(ClientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Email) && !model.Id.HasValue && string.IsNullOrEmpty(model.Password))
            {
                return new();
            }
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                using var context = new ConfectioneryDatabase();
                return context.Clients
                .Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Email))
                .Select(x => x.GetViewModel)
                .ToList();
            }
            else
            {
                using var context = new ConfectioneryDatabase();
                return context.Clients
                .Where(x => x.Id == model.Id)
                .Select(x => x.GetViewModel)
                .ToList();
            }
        }
        public ClientViewModel? GetElement(ClientSearchModel model)
        {
            if (string.IsNullOrEmpty(model.Email) && !model.Id.HasValue)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            return context.Clients
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.Email) && x.Email == model.Email) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }
        public ClientViewModel? Insert(ClientBindingModel model)
        {
            var newClient = Client.Create(model);
            if (newClient == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            context.Clients.Add(newClient);
            context.SaveChanges();
            return newClient.GetViewModel;
        }
        public ClientViewModel? Update(ClientBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var component = context.Clients.FirstOrDefault(x => x.Id == model.Id);
            if (component == null)
            {
                return null;
            }
            component.Update(model);
            context.SaveChanges();
            return component.GetViewModel;
        }
        public ClientViewModel? Delete(ClientBindingModel model)
        {
            using var context = new ConfectioneryDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
