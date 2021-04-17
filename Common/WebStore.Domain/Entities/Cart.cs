using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

        public int ItemCount => Items?.Sum(item => item.Quantity) ?? 0;
    }
}
