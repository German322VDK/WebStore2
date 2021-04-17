using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Entities.Oreders
{
    public class Order : NamedEntity
    {
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
    }
}
