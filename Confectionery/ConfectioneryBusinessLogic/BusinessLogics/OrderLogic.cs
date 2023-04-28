using ConfectioneryBusinessLogic.MailWorker;
using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly ILogger _logger;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractMailWorker _mailWorker;
        static readonly object _lock = new object();

        public OrderLogic(ILogger<OrderLogic> logger, IOrderStorage orderStorage, AbstractMailWorker mailWorker)
        {
            _logger = logger;
            _orderStorage = orderStorage;
            _mailWorker = mailWorker;
        }

        public bool CreateOrder(OrderBindingModel model)
        {
            CheckModel(model);
            if (model.Status != OrderStatus.Неизвестен) return false;
            model.Status = OrderStatus.Принят;
            var order = _orderStorage.Insert(model);
            if (order == null)
            {
                _logger.LogWarning("Insert operation failed");
                return false;
            }
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel()
            {
                MailAddress = order.ClientEmail,
                Subject = "Создан заказ №" + order.Id,
                Text = $"Создан заказ №{order.Id} от {order.DateCreate} на кондитерское изделие {order.PastryName} в количестве {order.Count} шт. Сумма заказа: {order.Sum}."

            });
            return true;
        }

        public bool DeliveryOrder(OrderBindingModel model)
        {
            CheckModel(model, false);
            var element = _orderStorage.GetElement(new OrderSearchModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                _logger.LogWarning("Read operation failed");
                return false;
            }
            if (element.Status != OrderStatus.Готов)
            {
                _logger.LogWarning("Status change operation failed");
                throw new InvalidOperationException("Заказ должен быть переведен в статус готовности перед выдачей!");
            }
            model.Status = OrderStatus.Выдан;
            var order = _orderStorage.Update(model);
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel()
            {
                MailAddress = order.ClientEmail,
                Subject = $"Заказ №{order.Id}. Статус изменен на Выдан",
                Text = $"Выдан заказ №{order.Id} от {order.DateCreate} на кондитерское изделие {order.PastryName} в количестве {order.Count} шт. Сумма заказа: {order.Sum}."

            });
            return true;
        }

        public bool FinishOrder(OrderBindingModel model)
        {
            CheckModel(model, false);
            var element = _orderStorage.GetElement(new OrderSearchModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                _logger.LogWarning("Read operation failed");
                return false;
            }
            if (element.Status != OrderStatus.Выполняется)
            {
                _logger.LogWarning("Status change operation failed");
                throw new InvalidOperationException("Заказ должен быть переведен в статус выполнения перед готовностью!");
            }
            model.Status = OrderStatus.Готов;
            model.DateImplement = DateTime.Now;
            var order = _orderStorage.Update(model);
            _mailWorker.MailSendAsync(new MailSendInfoBindingModel()
            {
                MailAddress = order.ClientEmail,
                Subject = $"Заказ №{order.Id}. Статус изменен на Готов",
                Text = $"Готов заказ №{order.Id} от {order.DateCreate} на кондитерское изделие {order.PastryName} в количестве {order.Count} шт. Сумма заказа: {order.Sum}."

            });
            return true;
        }

        public List<OrderViewModel>? ReadList(OrderSearchModel? model)
        {
            _logger.LogInformation("ReadList. Id:{Id}", model?.Id);
            var list = model == null ? _orderStorage.GetFullList() : _orderStorage.GetFilteredList(model);
            if (list == null)
            {
                _logger.LogWarning("ReadList return null list");
                return null;
            }
            _logger.LogInformation("ReadList. Count:{Count}", list.Count);
            return list;
        }

        public OrderViewModel? ReadElement(OrderSearchModel? model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _logger.LogInformation("ReadElement. ImplementerId:{ImplementerId}. OrderStatus:{OrderStatus}. Id:{Id}", model.ImplementerId, model.OrderStatus, model.Id);
            var element = _orderStorage.GetElement(model);
            if (element == null)
            {
                _logger.LogWarning("ReadElement element not found");
                return null;
            }
            _logger.LogInformation("ReadElement find. Id:{Id}", element.Id);
            return element;
        }

        public bool TakeOrderInWork(OrderBindingModel model)
        {
            lock (_lock)
            {
                CheckModel(model, false);
                var element = _orderStorage.GetElement(new OrderSearchModel
                {
                    Id = model.Id
                });
                if (element == null)
                {
                    _logger.LogWarning("Read operation failed");
                    return false;
                }
                if (element.Status != OrderStatus.Принят)
                {
                    _logger.LogWarning("Status change operation failed");
                    throw new InvalidOperationException("Заказ должен быть переведен в статус принятого перед его выполнением!");
                }
                model.Status = OrderStatus.Выполняется;
                var order = _orderStorage.Update(model);
                _mailWorker.MailSendAsync(new MailSendInfoBindingModel()
                {
                    MailAddress = order.ClientEmail,
                    Subject = $"Заказ №{order.Id}. Статус изменен на Выполняется",
                    Text = $"Выполняется заказ №{order.Id} от {order.DateCreate} на кондитерское изделие {order.PastryName} в количестве {order.Count} шт. Сумма заказа: {order.Sum}."

                });
                return true;
            }
        }

        private void CheckModel(OrderBindingModel model, bool withParams = true)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!withParams)
            {
                return;
            }
            if (model.Sum <= 0)
            {
                throw new ArgumentNullException("Сумма заказа должна быть больше 0", nameof(model.Sum));
            }
            if (model.Count <= 0)
            {
                throw new ArgumentNullException("Количество изделий должно быть больше 0", nameof(model.Count));
            }
            _logger.LogInformation("Order. Sum:{Sum}. Id:{Id}", model.Sum, model.Id);
            var element = _orderStorage.GetElement(new OrderSearchModel
            {
                Id = model.Id
            });
            if (element != null && element.Id != model.Id)
            {
                throw new InvalidOperationException("Заказ с таким ID уже существует");
            }
        }
    }
}
