using System;
using System.Collections.Generic;

namespace WebStore.Domain.DTO
{
    /// <summary>
    /// Информация о заказе
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя заказа
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефон заказчика
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Адрес заказчика
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Время заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Количество объектов заказа
        /// </summary>
        public IEnumerable<OrderItemDTO> Items { get; set; }
    }
}
