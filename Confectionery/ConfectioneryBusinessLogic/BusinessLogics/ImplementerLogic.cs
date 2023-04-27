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
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly ILogger _logger;
        private readonly IImplementerStorage _implementerStorage;

        public ImplementerLogic(ILogger<ImplementerLogic> logger, IImplementerStorage implementerStorage)
        {
            _logger = logger;
            _implementerStorage = implementerStorage;
        }

        public List<ImplementerViewModel>? ReadList(ImplementerSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}. ImplementerFIO:{ImplementerFIO}. Password:{Password}", model?.Id, model?.ImplementerFIO, model?.Password);
            var list = model == null ? _implementerStorage.GetFullList() : _implementerStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public ImplementerViewModel? ReadElement(ImplementerSearchModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. Id:{Id}. ImplementerFIO:{ImplementerFIO}. Password:{Password}", model.Id, model.ImplementerFIO, model.Password);
            var element = _implementerStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool Create(ImplementerBindingModel model)
        {
            CheckModel(model);
            if (_implementerStorage.Insert(model) == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            return true;
        }

        public bool Update(ImplementerBindingModel model)
        {
            CheckModel(model);
            if (_implementerStorage.Update(model) == null)
            {
                _logger.LogWarning("Update operation failed");
                return false;
            }
            return true;
        }
        public bool Delete(ImplementerBindingModel model)
        {
            CheckModel(model, false);
            _logger.LogInformation("Delete. Id:{Id}", model.Id);
            if (_implementerStorage.Delete(model) == null)
            {
                _logger.LogWarning("Delete operation failed");
                return false;
            }
            return true;
        }

        private void CheckModel(ImplementerBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (string.IsNullOrEmpty(model.ImplementerFIO))
            {
                throw new ArgumentNullException("Нет ФИО исполнителя", nameof(model.ImplementerFIO));
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Нет пароля исполнителя", nameof(model.Password));
            }
            if (model.WorkExperience < 0)
            {
                throw new ArgumentNullException("Опыт работы не может быть меньше 0!", nameof(model.WorkExperience));
            }
            if (model.Qualification < 0)
            {
                throw new ArgumentNullException("Квалификация не может быть меньше 0!", nameof(model.Qualification));
            }
            _logger.LogInformation("Implementer. ImplementerFIO:{ImplementerFIO}. Password:{Password}. Id:{Id}", model.ImplementerFIO, model.Password, model.Id);
            var element = _implementerStorage.GetElement(new ImplementerSearchModel
            {
                ImplementerFIO = model.ImplementerFIO
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Исполнитель с таким ФИО уже зарегестрирован");
            }
        }
    }
}
