using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    [DataContract]
    public class MessageInfo : IMessageInfoModel
    {
        [DataMember]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string MessageId { get; set; } = string.Empty;
        [DataMember]
        public int? ClientId { get; set; }
        [DataMember]
        [Required]
        public string SenderName { get; set; } = string.Empty;
        [DataMember]
        [Required]
        public DateTime DateDelivery { get; set; }
        [DataMember]
        [Required]
        public string Subject { get; set; } = string.Empty;
        [DataMember]
        [Required]
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
    }
}
