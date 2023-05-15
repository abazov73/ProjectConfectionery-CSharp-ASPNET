using ConfectioneryContracts.Attributes;
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
        [Column(visible:false)]
        public string MessageId { get; set; } = string.Empty;
        [Column(visible:false)]
        public int? ClientId { get; set; }
        [Column(title:"Отправитель", width:75)]
        public string SenderName { get; set; } = string.Empty;
        [Column(title:"Доставлено")]
        public DateTime DateDelivery { get; set; }
        [Column(title:"Заголовок", width:75)]
        public string Subject { get; set; } = string.Empty;
        [Column(title:"Текст", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string Body { get; set; } = string.Empty;

        public int Id { get; set; }
    }
}
