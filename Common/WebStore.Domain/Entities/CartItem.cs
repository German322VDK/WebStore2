using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.Entities
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public int Quentity { get; set; } = 1;
    }
}
