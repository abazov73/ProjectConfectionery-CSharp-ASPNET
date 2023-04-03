using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class ShopLogic : IShopLogic
    {
        private readonly ILogger _logger;
        private readonly IShopStorage _shopStorage;

        public ShopLogic(ILogger<ShopLogic> logger, IShopStorage shopStorage)
        {
            _logger = logger;
            _shopStorage = shopStorage;
        }

        public List<ShopViewModel>? ReadList(ShopSearchModel? model)
        {
            _logger.LogInformation("ReadList. ShopName:{ShopName}. Id:{Id}", model?.ShopName, model?.Id);
            var list = model == null ? _shopStorage.GetFullList() : _shopStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public ShopViewModel? ReadElement(ShopSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. ShopName:{ShopName}. Id:{Id}", model.ShopName, model.Id);
            var element = _shopStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(ShopBindingModel model)
        {
            CheckModel(model);
            if (_shopStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(ShopBindingModel model)
        {
            CheckModel(model);
            if (_shopStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(ShopBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_shopStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(ShopBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.ShopName))
            {
                throw new ArgumentNullException("Нет названия иагазина", nameof(model.ShopName));
            }
            if (string.IsNullOrEmpty(model.ShopAdress))
            {
                throw new ArgumentNullException("Нет адресса иагазина", nameof(model.ShopAdress));
            }
            _logger.LogInformation("Shop. ShopName:{ShopName}. Id:{Id}", model.ShopName, model.Id);
            var element = _shopStorage.GetElement(new ShopSearchModel
            {
                ShopName = model.ShopName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Магазин с таким названием уже есть");
            }
        }

        public bool DeliverPastryToShop(ShopBindingModel shopModel, IPastryModel pastryModel, int count)
        {
            CheckModel(shopModel, false);
            if (pastryModel == null)
            {
                throw new ArgumentNullException(nameof(pastryModel));
            }
            if (count <= 0)
            {
                throw new ArgumentNullException("Количество изделий в поставке должно быть больше нуля!");
            }
            var element = _shopStorage.GetElement(new ShopSearchModel
            {
                Id = shopModel.Id
            });
            if (element == null)
            {
                _logger.LogWarning("Read operation failed");
                return false;
            }
            _logger.LogInformation("Delivery. ShopName:{ShopName}. Id:{Id}. PastryName:{PastryName}. Count:{count}", element.ShopName, element.Id, pastryModel.PastryName, count);
            if (element.ShopPastries.TryGetValue(pastryModel.Id, out var shopPastry))
            {
                element.ShopPastries[pastryModel.Id] = (pastryModel, shopPastry.Item2 + count);
            }
            else
            {
                element.ShopPastries.Add(pastryModel.Id, (pastryModel, count));
            }
            _shopStorage.Update(new ShopBindingModel
            {
                Id = element.Id,
                ShopName = element.ShopName,
                ShopAdress = element.ShopAdress,
                OpeningDate = element.OpeningDate,
                ShopPastries = element.ShopPastries
            });
            return true;
        }
    }
}
