using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BindingModels
{
    public class MessageInfoBindingModel : IMessageInfoModel
    {
        public string MessageId { get; set; } = string.Empty;
        public int? ClientId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public DateTime DateDelivery { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
