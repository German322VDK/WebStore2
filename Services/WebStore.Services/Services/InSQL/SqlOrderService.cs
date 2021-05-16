using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Oreders;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;
using WebStrore.DAL.Context;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly UserManager<User> _UserManager;
        private readonly WebStoreDB _db;
        private readonly ILogger<SqlOrderService> _Logger;

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager, ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id))
            .ToDTO();

        public async Task<IEnumerable<OrderDTO>> GetUserOrders(string Username) => (await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .Where(order => order.User.UserName == Username)
            .ToArrayAsync())
            .Select(o => o.ToDTO());

        public async Task<OrderDTO> CreateOrder(string Username, CreateOrderModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(Username);

            if (user is null) 
                throw new InvalidOperationException($"Пользователь {Username} не найден в БД!");

            _Logger.LogInformation("Оформление нового заказа для {0}", Username);
            var timer = Stopwatch.StartNew();

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.OrderModel.Name,
                Address = OrderModel.OrderModel.Address,
                Phone = OrderModel.OrderModel.Phone,
                User = user,
            };

            foreach(var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);

                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quentity = item.Quentity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            _Logger.LogInformation("Заказ для {0} успешно сформирован за {1} с id {2} на сумму {3}", 
                Username, timer.Elapsed, order.Id, order.Items.Sum(i => i.TotalItemPrice));

            return order.ToDTO();
        }
    }
}
