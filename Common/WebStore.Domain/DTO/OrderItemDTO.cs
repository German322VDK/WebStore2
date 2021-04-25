namespace WebStore.Domain.DTO
{
    /// <summary>
    /// Объект заказа
    /// </summary>
    public class OrderItemDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Колличество
        /// </summary>
        public int Quentity { get; set; }
    }
}
