using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quentity)> Items { get; set; }

        public int ItemsCount => Items?.Sum(item => item.Quentity) ?? 0;

        public decimal TotalPrice => Items?.Sum(item => item.Product.Price * item.Quentity) ?? 0m;
    }
}
