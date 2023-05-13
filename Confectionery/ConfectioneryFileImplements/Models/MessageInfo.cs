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
    public class MessageInfo : IMessageInfoModel
    {
        [DataMember]
        public string MessageId { get; set; } = string.Empty;
        [DataMember]
        public int? ClientId { get; set; }
        [DataMember]
        public string SenderName { get; set; } = string.Empty;
        [DataMember]
        public DateTime DateDelivery { get; set; }
        [DataMember]
        public string Subject { get; set; } = string.Empty;
        [DataMember]
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

        public static MessageInfo? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new MessageInfo()
            {
                MessageId = element.Attribute("MessageId")!.Value,
                ClientId = Convert.ToInt32(element.Element("ClientId")!.Value),
                SenderName = element.Element("SenderName")!.Value,
                Body = element.Element("Body")!.Value,
                Subject = element.Element("Subject")!.Value,
                DateDelivery = Convert.ToDateTime(element.Element("DateDelivery")!.Value)
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

        public XElement GetXElement => new("MessageInfo",
        new XAttribute("MessageId", MessageId),
        new XElement("ClientId", ClientId.ToString()),
        new XElement("SenderName", SenderName),
        new XElement("Subject", Subject),
        new XElement("Body", Body),
        new XElement("DateDelivery", DateDelivery.ToString()));
    }
}
