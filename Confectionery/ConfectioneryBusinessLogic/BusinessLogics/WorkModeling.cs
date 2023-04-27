using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
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
    public class WorkModeling : IWorkProcess
    {
        private readonly ILogger _logger;

        private readonly Random _rnd;

        private IOrderLogic? _orderLogic;

        public WorkModeling(ILogger<WorkModeling> logger)
        {
            _logger = logger;
            _rnd = new Random(1000);
        }

        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
            var implementers = implementerLogic.ReadList(null);
            if (implementers == null)
            {
                _logger.LogWarning("DoWork. Implementers is null");
                return;
            }
            var orders = _orderLogic.ReadList(new OrderSearchModel { OrderStatus = OrderStatus.Принят });
            if (orders == null || orders.Count == 0)
            {
                _logger.LogWarning("DoWork. Orders is null or empty");
                return;
            }
            _logger.LogDebug("DoWork for {Count} orders", orders.Count);
            foreach (var implementer in implementers)
            {
                Task.Run(() => WorkerWorkAsync(implementer, orders));
            }
        }

        /// <summary>
        /// Иммитация работы исполнителя
        /// </summary>
        /// <param name="implementer"></param>
        /// <param name="orders"></param>
        private async Task WorkerWorkAsync(ImplementerViewModel implementer, List<OrderViewModel>? orders)
        {
            if (_orderLogic == null || implementer == null || orders == null)
            {
                return;
            }
            await RunOrderInWork(implementer);
            if (orders == null || orders.Count == 0)
                return;

            await Task.Run(async () =>
            {
                foreach (var order in orders)
                {
                    try
                    {
                        _logger.LogDebug("DoWork. Worker {Id} try get order {Order}", implementer.Id, order.Id);
                        // пытаемся назначить заказ на исполнителя
                        _orderLogic.TakeOrderInWork(new OrderBindingModel
                        {
                            Id = order.Id,
                            ImplementerId = implementer.Id
                        });
                        // делаем работу
                        await Task.Delay(implementer.WorkExperience * _rnd.Next(100, 1000) * order.Count);
                        _logger.LogDebug("DoWork. Worker {Id} finish order {Order}", implementer.Id, order.Id);
                        _orderLogic.FinishOrder(new OrderBindingModel
                        {
                            Id = order.Id
                        });
                    }
                    // кто-то мог уже перехватить заказ, игнорируем ошибку
                    catch (InvalidOperationException ex)
                    {
                        _logger.LogWarning(ex, "Error try get work");
                    }
                    // заканчиваем выполнение имитации в случае иной ошибки
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while do work");
                        throw;
                    }
                    // отдыхаем
                    await Task.Delay(implementer.Qualification * _rnd.Next(10, 100));
                }
            });
        }

        /// <summary>
        /// Ищем заказ, которые уже в работе (вдруг исполнителя прервали)
        /// </summary>
        /// <param name="implementer"></param>
        /// <returns></returns>
        private async Task RunOrderInWork(ImplementerViewModel implementer)
        {
            if (_orderLogic == null || implementer == null)
            {
                return;
            }
            try
            {
                var runOrder = await Task.Run(() => _orderLogic.ReadElement(new OrderSearchModel
                {
                    ImplementerId = implementer.Id,
                    OrderStatus = OrderStatus.Выполняется
                }));
                if (runOrder == null)
                {
                    return;
                }

                _logger.LogDebug("DoWork. Worker {Id} back to order {Order}", implementer.Id, runOrder.Id);
                // доделываем работу
                await Task.Delay(implementer.WorkExperience * _rnd.Next(100, 300) * runOrder.Count);
                _logger.LogDebug("DoWork. Worker {Id} finish order {Order}", implementer.Id, runOrder.Id);
                _orderLogic.FinishOrder(new OrderBindingModel
                {
                    Id = runOrder.Id
                });
                // отдыхаем
                await Task.Delay(implementer.Qualification * _rnd.Next(10, 100));
            }
            // заказа может не быть, просто игнорируем ошибку
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error try get work");
            }
            // а может возникнуть иная ошибка, тогда просто заканчиваем выполнение имитации
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while do work");
                throw;
            }
        }
    }
}
