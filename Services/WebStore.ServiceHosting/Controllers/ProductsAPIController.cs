using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Products)]
    [ApiController]
    public class ProductsAPIController : ControllerBase, IProductData
    {
        private readonly IProductData _ProductData;

        public ProductsAPIController(IProductData ProductData) => _ProductData = ProductData;

        [HttpGet("brands")]
        public IEnumerable<BrandDTO> GetBrands() => _ProductData.GetBrands();

        [HttpGet("{id:int}")]
        public ProductDTO GetProductById(int id) => _ProductData.GetProductById(id);

        [HttpPost]
        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) =>
            _ProductData.GetProducts(Filter);

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections() => _ProductData.GetSections();
    }
}
