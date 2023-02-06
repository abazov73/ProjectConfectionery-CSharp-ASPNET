using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using Microsoft.Extensions.Logging;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class IngredientLogic : IIngredientLogic
    {
        private readonly ILogger _logger;
        private readonly IIngredientStorage _ingredientStorage;

        public IngredientLogic(ILogger<IngredientLogic> logger, IIngredientStorage ingredientStorage)
        {
            _logger = logger;
            _ingredientStorage = ingredientStorage;
        }

        public List<IngredientViewModel>? ReadList(IngredientSearchModel? model)
        {
            _logger.LogInformation("ReadList. IngredientName:{IngredientName}. Id:{Id}", model?.IngredientName, model?.Id);
            var list = model == null ? _ingredientStorage.GetFullList() : _ingredientStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public IngredientViewModel? ReadElement(IngredientSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. IngredientName:{IngredientName}. Id:{Id}", model.IngredientName, model.Id);
            var element = _ingredientStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create (IngredientBindingModel model)
        {
            CheckModel(model);
            if (_ingredientStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(IngredientBindingModel model)
        {
            CheckModel(model);
            if (_ingredientStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(IngredientBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_ingredientStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(IngredientBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.IngredientName))
            {
                throw new ArgumentNullException("Нет названия компонента", nameof(model.IngredientName));
            }
            if (model.Cost <= 0)
            {
                throw new ArgumentNullException("Цена ингредиента должна быть больше 0", nameof(model.Cost));
            }
            _logger.LogInformation("Ingredient. IngredietnName:{IngredientName}. Cost:{Cost}. Id:{Id}", model.IngredientName, model.Cost, model.Id);
            var element = _ingredientStorage.GetElement(new IngredientSearchModel
            {
                IngredientName = model.IngredientName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Ингредиент с таким названием уже есть");
            }
        }
    }
}
