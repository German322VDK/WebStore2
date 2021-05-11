using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Components
{
    public class features_itemsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;

        public features_itemsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke() 
        {
            var products = _ProductData.GetProducts();

            return View(new CatalogViewModel
            {
                Products = products.Products
                    .OrderBy(p => p.Order).FromDTO().ToView()
            }.Products);
        }
    }
}
