using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.BindingModels
{
    public class ImplementerBindingModel : IImplementerModel
    {
        public int Id { get; set; }
        public string ImplementerFIO { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int WorkExperience { get; set; }
        public int Qualification { get; set; }
    }
}
