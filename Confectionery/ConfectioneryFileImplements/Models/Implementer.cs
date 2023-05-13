using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfectioneryFileImplement.Models
{
    [DataContract]
    public class Implementer : IImplementerModel
    {
        [DataMember]
        public int Id { get; private set; }
        [DataMember]
        public string ImplementerFIO { get; set; } = string.Empty;
        [DataMember]
        public string Password { get; set; } = string.Empty;
        [DataMember]
        public int WorkExperience { get; set; }
        [DataMember]
        public int Qualification { get; set; }

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
        public static Implementer? Create(XElement element)
        {
            if (element == null)
            {
                return null;
            }
            return new Implementer()
            {
                Id = Convert.ToInt32(element.Attribute("Id")!.Value),
                ImplementerFIO = element.Attribute("ImplementerFIO")!.Value,
                Password = element.Attribute("Password")!.Value,
                WorkExperience = Convert.ToInt32(element.Attribute("WorkExperience")!.Value),
                Qualification = Convert.ToInt32(element.Attribute("Qualification")!.Value),
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
            Qualification = Qualification
        };
        public XElement GetXElement => new("Implementer",
        new XAttribute("Id", Id),
        new XElement("ImplementerFIO", ImplementerFIO),
        new XElement("Password", Password),
        new XElement("WorkExperience", WorkExperience),
        new XElement("Qualification", Qualification));
    }
}
