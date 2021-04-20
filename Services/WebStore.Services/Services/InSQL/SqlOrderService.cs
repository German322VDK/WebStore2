using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public SqlOrderService(WebStoreDB db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
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

            if (user is null) throw new InvalidOperationException($"Пользователь {Username} не найден в БД!");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.OrderModel.Name,
                Address = OrderModel.OrderModel.Address,
                Phone = OrderModel.OrderModel.Phone,
                User = user,
            };

            //var product_ids = Cart.Items.Select(items => items.Product.Id).ToArray();

            //var cart_products = await _db.Products
            //    .Where(p => product_ids.Contains(p.Id))
            //    .ToArrayAsync();

            //order.Items = Cart.Items.Join(
            //    cart_products,
            //    cart_item => cart_item.Product.Id,
            //    product => product.Id,
            //    (cart_item, product) => new OrderItem
            //    {
            //        Order = order,
            //        Product = product,
            //        Quantity = cart_item.Quentity,
            //        Price = product.Price, // можно сделать скидки
            //    }).ToArray();

            foreach(var item in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(item.Id);

                if (product is null) continue;

                var order_item = new OrderItem
                {
                    Order = order,
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = product
                };
                order.Items.Add(order_item);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order.ToDTO();
        }
    }
}
