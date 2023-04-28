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
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new ConfectioneryDatabase();
            return context.MessageInfos
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoSearchModel model)
        {
            if (string.IsNullOrEmpty(model.MessageId) && !model.ClientId.HasValue)
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return context.MessageInfos
                .Where(x => x.ClientId == model.ClientId)
                .Select(x => x.GetViewModel)
                .ToList();
        }
        public MessageInfoViewModel? GetElement(MessageInfoSearchModel model)
        {
            if (string.IsNullOrEmpty(model.MessageId) && !model.ClientId.HasValue)
            {
                return new();
            }
            using var context = new ConfectioneryDatabase();
            return context.MessageInfos
            .FirstOrDefault(x => x.MessageId.Equals(model.MessageId) && x.ClientId == model.ClientId)
            ?.GetViewModel;
        }
        public MessageInfoViewModel? Insert(MessageInfoBindingModel model)
        {
            var newMessageInfo = MessageInfo.Create(model);
            if (newMessageInfo == null)
            {
                return null;
            }
            using var context = new ConfectioneryDatabase();
            context.MessageInfos.Add(newMessageInfo);
            context.SaveChanges();
            return newMessageInfo.GetViewModel;
        }
    }
}
