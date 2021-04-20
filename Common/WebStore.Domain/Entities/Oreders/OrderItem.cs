using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities.Oreders
{
    public class OrderItem : Entity
    {
        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quentity { get; set; }

        [NotMapped]
        public decimal TotalItemPrice => Quentity * Price;
    }
}
