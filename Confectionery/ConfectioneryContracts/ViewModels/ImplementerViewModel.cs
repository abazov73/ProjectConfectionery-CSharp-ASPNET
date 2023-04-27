using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.ViewModels
{
    public class ImplementerViewModel : IImplementerModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО")]
        public string ImplementerFIO { get; set; } = String.Empty;
        [DisplayName("Пароль")]
        public string Password { get; set; } = String.Empty;
        [DisplayName("Опыт работы")]
        public int WorkExperience { get; set; }
        [DisplayName("Квалификация")]
        public int Qualification { get; set; }
    }
}
