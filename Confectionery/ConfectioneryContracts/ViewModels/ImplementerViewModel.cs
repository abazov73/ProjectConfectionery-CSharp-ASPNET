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
    public class ImplementerViewModel : IImplementerModel
    {
        [Column(visible:false)]
        public int Id { get; set; }
        [Column(title:"ФИО", gridViewAutoSize:GridViewAutoSize.Fill, isUseAutoSize:true)]
        public string ImplementerFIO { get; set; } = String.Empty;
        [Column(title:"Пароль", width:150)]
        public string Password { get; set; } = String.Empty;
        [Column(title:"Опыт работы", width:120)]
        public int WorkExperience { get; set; }
        [Column(title:"Квалификация", width:120)]
        public int Qualification { get; set; }
    }
}
