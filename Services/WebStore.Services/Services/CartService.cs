using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Services
{
    public class CartService : ICartService
    {
        private readonly ICartStore _CartStore;
        private readonly IProductData _ProductData;

        public CartService(ICartStore CartStore, IProductData ProductData)
        {
            _CartStore = CartStore;
            _ProductData = ProductData;
        }

        public void Add(int id)
        {
            var cart = _CartStore.Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id });
            else
                item.Quentity++;

            _CartStore.Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = _CartStore.Cart;

            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            if (item.Quentity > 0)
                item.Quentity--;

            if (item.Quentity <= 0)
                cart.Items.Remove(item);

            _CartStore.Cart = cart;
        }

        public void Remove(int id)
        {
            var cart = _CartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;
            cart.Items.Remove(item);
            _CartStore.Cart = cart;
        }

        public void Clear()
        {
            var cart = _CartStore.Cart;
            cart.Items.Clear();
            _CartStore.Cart = cart;
        }

        public CartViewModel GetViewModel()
        {
            var cart = _CartStore.Cart;
            var products = _ProductData.GetProducts(new ProductFilter
            {
                Ids = cart.Items.Select(item => item.ProductId).ToArray()
            });

            var products_views = products.Products.FromDTO().ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = cart.Items
                   .Where(item => products_views.ContainsKey(item.ProductId))
                   .Select(item => (products_views[item.ProductId], item.Quentity))
            };
        }
    }
}
