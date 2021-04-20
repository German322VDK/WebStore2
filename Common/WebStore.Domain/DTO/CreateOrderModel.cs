﻿using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.Domain.DTO
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderModel { get; set; }

        public List<OrderItemDTO> Items { get; set; }
    }
}
