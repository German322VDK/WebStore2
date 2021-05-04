using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUserOrders(string Username);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string Username, CreateOrderModel OrderModel);
    }
}
