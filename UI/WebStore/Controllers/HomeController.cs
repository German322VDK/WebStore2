using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductData _ProductData;

        public HomeController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index()
        {
            var products = _ProductData.GetProducts();

            return View(new CatalogViewModel
            {
                Products = products
                    .OrderBy(p => p.Order).FromDTO().ToView()
            });
        }

        public IActionResult Error() => View();

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
