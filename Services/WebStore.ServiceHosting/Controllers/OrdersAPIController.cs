using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>
    /// Управление заказами
    /// </summary>

    [Route(WebAPI.Orders)]
    [ApiController]
    public class OrdersAPIController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;

        public OrdersAPIController(IOrderService OrderService) => _OrderService = OrderService;

        /// <summary>
        /// Получение всех заказов указанного пользователя
        /// </summary>
        /// <param name="UserName">Имя пользователя</param>
        /// <returns>Перечень заказов этого пользователя</returns>
        [HttpGet("user/{UserName}")]
        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string UserName) =>
            await _OrderService.GetUserOrders(UserName);

        /// <summary>
        /// Получение заказа по его ID
        /// </summary>
        /// <param name="id">его ID</param>
        /// <returns>Заказ</returns>
        [HttpGet("{id:int}")]
        public async Task<OrderDTO> GetOrderById(int id) => await _OrderService.GetOrderById(id);

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <param name="UserName">Владелец этого заказа</param>
        /// <param name="OrderModel">Модель заказа</param>
        /// <returns>Заказ</returns>
        [HttpPost("{UserName}")]
        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel) =>
            await _OrderService.CreateOrder(UserName, OrderModel);
    }
}
