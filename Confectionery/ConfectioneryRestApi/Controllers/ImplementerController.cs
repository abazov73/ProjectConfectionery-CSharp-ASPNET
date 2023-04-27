using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.ViewModels;
using ConfectioneryDataModels.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ConfectioneryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImplementerController : Controller
    {
        private readonly ILogger _logger;

        private readonly IOrderLogic _order;

        private readonly IImplementerLogic _logic;

        public ImplementerController(IOrderLogic order, IImplementerLogic logic, ILogger<ImplementerController> logger)
        {
            _logger = logger;
            _order = order;
            _logic = logic;
        }

        [HttpGet]
        public ImplementerViewModel? Login(string login, string password)
        {
            try
            {
                return _logic.ReadElement(new ImplementerSearchModel
                {
                    ImplementerFIO = login,
                    Password = password
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка авторизации сотрудника");
                throw;
            }
        }

        [HttpGet]
        public List<OrderViewModel>? GetNewOrders()
        {
            try
            {
                return _order.ReadList(new OrderSearchModel
                {
                    OrderStatus = OrderStatus.Принят
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения новых заказов");
                throw;
            }
        }

        [HttpGet]
        public OrderViewModel? GetImplementerOrder(int implementerId)
        {
            try
            {
                return _order.ReadElement(new OrderSearchModel
                {
                    ImplementerId = implementerId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения текущего заказа исполнителя");
                throw;
            }
        }

        [HttpPost]
        public void TakeOrderInWork(OrderBindingModel model)
        {
            try
            {
                _order.TakeOrderInWork(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка перевода заказа с №{Id} в работу", model.Id);
                throw;
            }
        }

        [HttpPost]
        public void FinishOrder(OrderBindingModel model)
        {
            try
            {
                _order.FinishOrder(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отметки о готовности заказа с №{Id}", model.Id);
                throw;
            }
        }
    }
}
