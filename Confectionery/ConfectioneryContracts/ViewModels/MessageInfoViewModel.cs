using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class MessageInfoViewModel : IMessageInfoModel
    {
        public string MessageId { get; set; } = string.Empty;
        public int? ClientId { get; set; }
        [DisplayName("Отправитель")]
        public string SenderName { get; set; } = string.Empty;
        [DisplayName("Доставлено")]
        public DateTime DateDelivery { get; set; }
        [DisplayName("Заголовок")]
        public string Subject { get; set; } = string.Empty;
        [DisplayName("Текст")]
        public string Body { get; set; } = string.Empty;
    }
}
