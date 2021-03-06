using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand brand) => brand is null ? null :
            new BrandDTO
            {
                Id = brand.Id,
                Name = brand.Name,
                Order = brand.Order,
                ProductCount = brand.Products.Count()
            };

        public static Brand FromDTO(this BrandDTO brand) => brand is null ? null :
           new Brand
           {
               Id = brand.Id,
               Name = brand.Name,
               Order = brand.Order,
           };

        public static IEnumerable<BrandDTO> ToDTO(this IEnumerable<Brand> products) =>
            products.Select(ToDTO);

        public static IEnumerable<Brand> FromDTO(this IEnumerable<BrandDTO> products) =>
           products.Select(FromDTO);
    }
}
