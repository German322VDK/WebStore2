﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders([FromServices] IOrderService OrderService)
        {
            var orders = await OrderService.GetUserOrders(User.Identity!.Name);

            return View(orders.Select(order => new UserOrderViewModel 
            { 
                Id = order.Id,
                Name = order.Name,
                Address = order.Address,
                Phone = order.Phone,
                TotalPrice = order.Items.Sum(item => item.FromDTO().TotalItemPrice)
            }));
        }
    }
}
