using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataFileSingleton source;
        public MessageInfoStorage()
        {
            source = DataFileSingleton.GetInstance();
        }
        public List<MessageInfoViewModel> GetFullList()
        {
            return source.MessageInfos
            .Select(x => x.GetViewModel)
            .ToList();
        }
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoSearchModel model)
        {
            if (string.IsNullOrEmpty(model.MessageId) && !model.ClientId.HasValue)
            {
                return new();
            }
            return source.MessageInfos
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
            return source.MessageInfos
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
            source.MessageInfos.Add(newMessageInfo);
            source.SaveMessageInfos();
            return newMessageInfo.GetViewModel;
        }
    }
}
