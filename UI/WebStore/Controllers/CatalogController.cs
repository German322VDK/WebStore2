using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain;
using WebStore.Interfaces.Services;
using WebStore.Domain.ViewModels;
using WebStore.Services.Mapping;
using Microsoft.Extensions.Configuration;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IProductData _ProductData;
        private readonly IConfiguration _Configuration;

        public CatalogController(IProductData ProductData, IConfiguration Configuration)
        {
            _ProductData = ProductData;
            _Configuration = Configuration;
        }

        public IActionResult Index(int? BrandId, int? SectionId, int Page = 1, int? PageSize = null)
        {
            var page_size = PageSize ??
                (int.TryParse(_Configuration["CatalogPageSize"], out var value) ? value : null);

            var filter = new ProductFilter
            {
                BrandId = BrandId,
                SectionId = SectionId,
                Page = Page,
                PageSize = page_size
            };

            var products = _ProductData.GetProducts(filter);

            return View(new CatalogViewModel 
            { 
                SectionId = SectionId,
                BrandId = BrandId,
                Products = products.Products
                    .OrderBy(p => p.Order).FromDTO().ToView(),
                PageViewModel = new PageViewModel
                {
                    Page = Page,
                    PageSize = page_size ?? 0,
                    TotalItems = products.TotalCount
                }
            });
        }

        public IActionResult Details(int id)
        {
            var product = _ProductData.GetProductById(id);

            return View(product.FromDTO().ToView());
        }
    }
}
