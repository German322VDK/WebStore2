using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData ProductData) => _ProductData = ProductData;

        public IActionResult Index() => View(_ProductData.GetProducts().Products.FromDTO());

        public IActionResult Edit(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null) return NotFound();

            return View(product.FromDTO());
        }

        public IActionResult Delete(int id)
        {
            var product = _ProductData.GetProductById(id);

            if (product is null) return NotFound();

            return View(product.FromDTO());
        }
    }
}
