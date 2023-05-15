using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Models
{
    public class MessageInfo : IMessageInfoModel
    {
        public string MessageId { get; set; } = string.Empty;
        public int? ClientId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public static MessageInfo? Create(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new MessageInfo()
            {
                MessageId = model.MessageId,
                ClientId = model.ClientId,
                SenderName = model.SenderName,
                Body = model.Body,
                Subject = model.Subject,
                DateDelivery = model.DateDelivery
            };
        }

        public MessageInfoViewModel GetViewModel => new()
        {
            MessageId = MessageId,
            ClientId = ClientId,
            SenderName = SenderName,
            Body = Body,
            Subject = Subject,
            DateDelivery = DateDelivery
        };

        public int Id { get; set; }
    }
}
