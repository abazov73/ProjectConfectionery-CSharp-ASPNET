using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryFileImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly DataFileSingleton source;
        public PastryStorage()
        {
            source = DataFileSingleton.GetInstance();
        }
        public List<PastryViewModel> GetFullList()
        {
            return source.Pastries
            .Select(x => x.GetViewModel)
            .ToList();
        }

        public List<PastryViewModel> GetFilteredList(PastrySearchModel model)
        {
            if (string.IsNullOrEmpty(model.PastryName))
            {
                return new();
            }
            return source.Pastries
            .Where(x => x.PastryName.Contains(model.PastryName))
            .Select(x => x.GetViewModel)
            .ToList();
        }

        public PastryViewModel? GetElement(PastrySearchModel model)
        {
            if (string.IsNullOrEmpty(model.PastryName) && !model.Id.HasValue)
            {
                return null;
            }
            return source.Pastries
            .FirstOrDefault(x => (!string.IsNullOrEmpty(model.PastryName) && x.PastryName == model.PastryName) ||
            (model.Id.HasValue && x.Id == model.Id))
            ?.GetViewModel;
        }

        public PastryViewModel? Insert(PastryBindingModel model)
        {
            model.Id = source.Pastries.Count > 0 ? source.Pastries.Max(x => x.Id) + 1 : 1;
            var newPastry = Pastry.Create(model);
            if (newPastry == null)
            {
                return null;
            }
            source.Pastries.Add(newPastry);
            source.SavePastries();
            return newPastry.GetViewModel;
        }

        public PastryViewModel? Update(PastryBindingModel model)
        {
            var pastry = source.Pastries.FirstOrDefault(x => x.Id == model.Id);
            if (pastry == null)
            {
                return null;
            }
            pastry.Update(model);
            source.SavePastries();
            return pastry.GetViewModel;
        }

        public PastryViewModel? Delete(PastryBindingModel model)
        {
            var element = source.Pastries.FirstOrDefault(x => x.Id == model.Id);
            if (element != null)
            {
                source.Pastries.Remove(element);
                source.SavePastries();
                return element.GetViewModel;
            }
            return null;
        }
    }
}
