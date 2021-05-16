using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO
{
    /// <summary>
    /// Модель процесса создания заказа
    /// </summary>
    public class CreateOrderModel
    {
        /// <summary>
        /// Модель заказа
        /// </summary>
        public OrderViewModel OrderModel { get; set; }

        /// <summary>
        /// Перечень элементов заказа
        /// </summary>
        public List<OrderItemDTO> Items { get; set; }
    }
}
