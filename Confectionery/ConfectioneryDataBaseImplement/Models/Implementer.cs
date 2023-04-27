using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryDataBaseImplement.Models
{
    public class Implementer : IImplementerModel
    {
        public int Id { get; private set; }
        [Required]
        public string ImplementerFIO { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int WorkExperience { get; set; }
        [Required]
        public int Qualification { get; set; }
        [ForeignKey("ImplementerId")]
        public virtual List<Order> Orders { get; set; } = new List<Order>();

        public static Implementer? Create(ImplementerBindingModel? model)
        {
            if (model == null)
            {
                return null;
            }
            return new Implementer()
            {
                Id = model.Id,
                ImplementerFIO = model.ImplementerFIO,
                Password = model.Password,
                WorkExperience = model.WorkExperience,
                Qualification = model.Qualification,
            };
        }
        public void Update(ImplementerBindingModel? model)
        {
            if (model == null)
            {
                return;
            }
            ImplementerFIO = model.ImplementerFIO;
            Password = model.Password;
            WorkExperience = model.WorkExperience;
            Qualification = model.Qualification;
        }
        public ImplementerViewModel GetViewModel => new()
        {
            Id = Id,
            ImplementerFIO = ImplementerFIO,
            Password = Password,
            WorkExperience = WorkExperience,
            Qualification = Qualification,
        };
    }
}
