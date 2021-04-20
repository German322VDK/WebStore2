using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, WebAPI.Orders) { }

        public async Task<OrderDTO> CreateOrder(string Username, CreateOrderModel OrderModel)
        {
            var response = await PostAsync($"{Address}/{Username}", OrderModel);

            return await response.Content.ReadAsAsync<OrderDTO>();
        }


        public async Task<OrderDTO> GetOrderById(int id) =>
            await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string Username) =>
            await GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{Username}");

    }
}
