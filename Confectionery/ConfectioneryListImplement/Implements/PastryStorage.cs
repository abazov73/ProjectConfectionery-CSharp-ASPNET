using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryListImplement.Implements
{
    public class PastryStorage : IPastryStorage
    {
        private readonly DataListSingleton _source;
        public PastryStorage()
        {
            _source = DataListSingleton.GetInstance();
        }
        public List<PastryViewModel> GetFullList()
        {
            var result = new List<PastryViewModel>();
            foreach (var pastry in _source.Pastries)
            {
                result.Add(pastry.GetViewModel);
            }
            return result;
        }

        public List<PastryViewModel> GetFilteredList(PastrySearchModel model)
        {
            var result = new List<PastryViewModel>();
            if (string.IsNullOrEmpty(model.PastryName))
            {
                return result;
            }
            foreach (var pastry in _source.Pastries)
            {
                if (pastry.PastryName.Contains(model.PastryName))
                {
                    result.Add(pastry.GetViewModel);
                }
            }
            return result;
        }

        public PastryViewModel? GetElement(PastrySearchModel model)
        {
            if (string.IsNullOrEmpty(model.PastryName) && !model.Id.HasValue)
            {
                return null;
            }
            foreach (var pastry in _source.Pastries)
            {
                if ((!string.IsNullOrEmpty(model.PastryName) && pastry.PastryName == model.PastryName) ||
                (model.Id.HasValue && pastry.Id == model.Id))
                {
                    return pastry.GetViewModel;
                }
            }
            return null;
        }

        public PastryViewModel? Insert(PastryBindingModel model)
        {
            model.Id = 1;
            foreach (var pastry in _source.Pastries)
            {
                if (model.Id <= pastry.Id)
                {
                    model.Id = pastry.Id + 1;
                }
            }
            var newPastry = Pastry.Create(model);
            if (newPastry == null)
            {
                return null;
            }
            _source.Pastries.Add(newPastry);
            return newPastry.GetViewModel;
        }

        public PastryViewModel? Update(PastryBindingModel model)
        {
            foreach (var pastry in _source.Pastries)
            {
                if (pastry.Id == model.Id)
                {
                    pastry.Update(model);
                    return pastry.GetViewModel;
                }
            }
            return null;
        }

        public PastryViewModel? Delete(PastryBindingModel model)
        {
            for (int i = 0; i < _source.Pastries.Count; ++i)
            {
                if (_source.Pastries[i].Id == model.Id)
                {
                    var element = _source.Pastries[i];
                    _source.Pastries.RemoveAt(i);
                    return element.GetViewModel;
                }
            }
            return null;
        }
    }
}
