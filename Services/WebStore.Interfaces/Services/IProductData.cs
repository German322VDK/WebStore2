using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.DTO;

namespace WebStore.Interfaces.Services
{
    public interface IProductData
    {
        IEnumerable<SectionDTO> GetSections();

        SectionDTO GetSectionById(int id);

        BrandDTO GetBrandById(int id);

        IEnumerable<BrandDTO> GetBrands();

        PageProductsDTO GetProducts(ProductFilter Filter = null);

        ProductDTO GetProductById(int id);
    }
}
