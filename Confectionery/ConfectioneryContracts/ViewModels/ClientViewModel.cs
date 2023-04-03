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
        public int Id { get; set; }
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; } = string.Empty;
        [DisplayName("Логин (эл. почта)")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Пароль")]
        public string Password { get; set; } = string.Empty;
    }
}
