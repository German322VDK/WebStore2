using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Oreders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
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

        public async Task<Order> GetOrderById(int id) => await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<IEnumerable<Order>> GetUserOrders(string Username) => await _db.Orders
            .Include(order => order.User)
            .Include(order => order.Items)
            .Where(order => order.User.UserName == Username)
            .ToArrayAsync();

        public async Task<Order> CreateOrder(string Username, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(Username);

            if (user is null) throw new InvalidOperationException($"Пользователь {Username} не найден в БД!");

            await using var transaction = await _db.Database.BeginTransactionAsync().ConfigureAwait(false);

            var order = new Order
            {
                Name = OrderModel.Name,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                User = user,
            };

            var product_ids = Cart.Items.Select(items => items.Product.Id).ToArray();

            var cart_products = await _db.Products
                .Where(p => product_ids.Contains(p.Id))
                .ToArrayAsync();

            order.Items = Cart.Items.Join(
                cart_products,
                cart_item => cart_item.Product.Id,
                product => product.Id,
                (cart_item, product) => new OrderItem
                {
                    Order = order,
                    Product = product,
                    Quentity = cart_item.Quentity,
                    Price = product.Price, // можно сделать скидки
                }).ToArray();

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            return order;
        }
    }
}
