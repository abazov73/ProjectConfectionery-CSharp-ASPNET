using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class PastryLogic : IPastryLogic
    {
        private readonly ILogger _logger;
        private readonly IPastryStorage _pastryStorage;

        public PastryLogic(ILogger<IngredientLogic> logger, IPastryStorage pastryStorage)
        {
            _logger = logger;
            _pastryStorage = pastryStorage;
        }

        public List<PastryViewModel>? ReadList(PastrySearchModel? model)
        {
            _logger.LogInformation("ReadList. PastryName:{PastryName}. Id:{Id}", model?.PastryName, model?.Id);
            var list = model == null ? _pastryStorage.GetFullList() : _pastryStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public PastryViewModel? ReadElement(PastrySearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. PastryName:{PastryName}. Id:{Id}", model.PastryName, model.Id);
            var element = _pastryStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(PastryBindingModel model)
        {
            CheckModel(model);
            if (_pastryStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(PastryBindingModel model)
        {
            CheckModel(model);
            if (_pastryStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(PastryBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_pastryStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(PastryBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.PastryName))
            {
                throw new ArgumentNullException("Нет названия изделия", nameof(model.PastryName));
            }
            if (model.Price <= 0)
            {
                throw new ArgumentNullException("Цена изделия должна быть больше 0", nameof(model.Price));
            }
            if (model.PastryIngredients == null || model.PastryIngredients.Count == 0)
            {
                throw new ArgumentNullException("Нет ингредиентов изделия", nameof(model.PastryIngredients));
            }
            _logger.LogInformation("Pastry. PastryName:{PastryName}. Cost:{Cost}. Id:{Id}", model.PastryName, model.Price, model.Id);
            var element = _pastryStorage.GetElement(new PastrySearchModel
            {
                PastryName = model.PastryName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Изделие с таким названием уже есть");
            }
        }
    }
}
