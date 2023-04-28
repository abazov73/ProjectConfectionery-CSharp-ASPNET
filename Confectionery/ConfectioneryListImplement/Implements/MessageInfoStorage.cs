using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton _source;

        public MessageInfoStorage(DataListSingleton source)
        {
            _source = source;
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in _source.MessageInfos)
            {
                result.Add(messageInfo.GetViewModel);
            }
            return result;
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoSearchModel model)
        {
            var result = new List<MessageInfoViewModel>();
            if (string.IsNullOrEmpty(model.MessageId) && !model.ClientId.HasValue)
            {
                return result;
            }
            foreach (var messageInfo in _source.MessageInfos)
            {
                if (messageInfo.ClientId == model.ClientId)
                {
                    result.Add(messageInfo.GetViewModel);
                }
            }
            return result;
        }

        public MessageInfoViewModel? GetElement(MessageInfoSearchModel model)
        {
            if (string.IsNullOrEmpty(model.MessageId) && !model.ClientId.HasValue)
            {
                return null;
            }
            foreach (var messageInfo in _source.MessageInfos)
            {
                if (!string.IsNullOrEmpty(model.MessageId) && model.ClientId.HasValue && messageInfo.MessageId == model.MessageId && messageInfo.ClientId == model.ClientId)
                {
                    return messageInfo.GetViewModel;
                }
            }
            return null;
        }

        public MessageInfoViewModel? Insert(MessageInfoBindingModel model)
        {
            var newMessageInfo = MessageInfo.Create(model);
            if (newMessageInfo == null)
            {
                return null;
            }
            _source.MessageInfos.Add(newMessageInfo);
            return newMessageInfo.GetViewModel;
        }
    }
}
