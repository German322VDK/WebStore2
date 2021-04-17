using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Oreders;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrders(string Username);

        Task<Order> GetOrderById(int id);

        Task<Order> CreateOrder(string Username, CartViewModel Cart, OrderViewModel OrderModel);
    }
}
