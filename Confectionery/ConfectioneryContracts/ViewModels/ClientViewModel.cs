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
    public class ClientViewModel : IClientModel
    {
        [Column(visible:false)]
        public int Id { get; set; }
        [Column(title:"ФИО клиента", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string ClientFIO { get; set; } = string.Empty;
        [Column(title:"Логин (эл. почта)", width:200)]
        public string Email { get; set; } = string.Empty;
        [Column(title:"Пароль", width:150)]
        public string Password { get; set; } = string.Empty;
    }
}
