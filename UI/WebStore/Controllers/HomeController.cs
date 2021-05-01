﻿using Microsoft.AspNetCore.Mvc;
using System;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Error() => View();

        public IActionResult Throw() => throw new ApplicationException("Test Error!");

        public IActionResult Contact_us() => View();

        public IActionResult Login() => View();

        public IActionResult Checkout() => View();

        public IActionResult Cart() => View();

        public IActionResult Blog() => View();

        public IActionResult Blog_single() => View();

        public IActionResult Shop() => View(); 

        public IActionResult Product_details() => View();
    }
}
