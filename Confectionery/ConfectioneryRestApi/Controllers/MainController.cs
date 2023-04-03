using ConfectioneryContracts.BindingModels;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.SearchModels;
using ConfectioneryContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConfectioneryRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : Controller
    {
        private readonly ILogger _logger;

        private readonly IOrderLogic _order;

        private readonly IPastryLogic _pastry;

        public MainController(ILogger<MainController> logger, IOrderLogic order, IPastryLogic pastry)
        {
            _logger = logger;
            _order = order;
            _pastry = pastry;
        }

        [HttpGet]
        public List<PastryViewModel>? GetPastryList()
        {
            try
            {
                return _pastry.ReadList(null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка продуктов");
                throw;
            }
        }

        [HttpGet]
        public PastryViewModel? GetPastry(int pastryId)
        {
            try
            {
                return _pastry.ReadElement(new PastrySearchModel { Id = pastryId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения продукта по id={Id}", pastryId);
                throw;
            }
        }

        [HttpGet]
        public List<OrderViewModel>? GetOrders(int clientId)
        {
            try
            {
                return _order.ReadList(new OrderSearchModel { ClientId = clientId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка получения списка заказов клиента id={Id}", clientId);
                throw;
            }
        }

        [HttpPost]
        public void CreateOrder(OrderBindingModel model)
        {
            try
            {
                _order.CreateOrder(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка создания заказа");
                throw;
            }
        }
    }
}
