using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStrore.DAL.Context;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SqlProductData : IProductData
    {
       private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Brand> GetBrands() => _db.Brands.Include(b => b.Products);

        public IEnumerable<Section> GetSections() => _db.Sections.Include(s => s.Products);

        public IEnumerable<Product> GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
                .Include(p => p.Brand)
               .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
            {
                query = query.Where(products => Filter.Ids.Contains(products.Id));
            }
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }

            return query;
        }

        public Product GetProductById(int id) => _db.Products
            .Include(product => product.Brand)
            .Include(product => product.Section)
            .FirstOrDefault(product => product.Id == id);
    }
}
