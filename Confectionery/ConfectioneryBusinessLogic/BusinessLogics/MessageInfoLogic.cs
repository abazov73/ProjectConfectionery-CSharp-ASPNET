using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly ILogger _logger;
        private readonly IMessageInfoStorage _messageStorage;

        public MessageInfoLogic(ILogger logger, IMessageInfoStorage logic)
        {
            _logger = logger;
            _messageStorage = logic;
        }

        public List<MessageInfoViewModel>? ReadList(MessageInfoSearchModel? model)
        {
            _logger.LogInformation("ReadList. MessageId:{MessageId}. ClientId:{ClientId}.", model?.MessageId, model?.ClientId);
            var list = model == null ? _messageStorage.GetFullList() : _messageStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public bool Create(MessageInfoBindingModel model)
        {
            CheckModel(model);
            if (_messageStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(MessageInfoBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.MessageId))
            {
                throw new ArgumentNullException("Нет id письма", nameof(model.MessageId));
            }
            if (string.IsNullOrEmpty(model.SenderName))
            {
                throw new ArgumentNullException("Нет отправителя", nameof(model.SenderName));
            }
            if (string.IsNullOrEmpty(model.Subject))
            {
                throw new ArgumentNullException("Нет темы", nameof(model.Subject));
            }
            if (string.IsNullOrEmpty(model.Body))
            {
                throw new ArgumentNullException("Нет текста письма", nameof(model.Subject));
            }
            _logger.LogInformation("MessageInfo. MessageId:{MessageId}. SenderName:{SenderName}. Subject:{Subject}. Body:{Body}", model.MessageId, model.SenderName, model.Subject, model.Body);
        }
    }
}
