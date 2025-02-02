using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister]

    public class ProductRepository : IProductRepository
    {
        private static readonly List<Product> _products = new List<Product>();

        /*static ProductRepository()
        {
            _products.Add(new Product("Laptop", 999.99m, 10));
            _products.Add(new Product("Smartphone", 599.99m, 15));
        }*/

        // Create
        public Product Create(Product product)
        {
            _products.Add(product);
            return product;
        }

        public bool Update(Guid id, Product updatedProduct)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.SetName(updatedProduct.Name);
            existing.SetPrice(updatedProduct.Price);
            existing.SetStockQuantity(updatedProduct.StockQuantity);
            return true;
        }

        // Delete
        public bool Delete(Guid id)
        {
            var product = GetById(id);
            if (product == null) return false;

            _products.Remove(product);
            return true;
        }

        // Get by Id
        public Product GetById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetList(string name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            return _products
                .Where(p => (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
                            (!minPrice.HasValue || p.Price >= minPrice) &&
                            (!maxPrice.HasValue || p.Price <= maxPrice))
                .ToList();
        }

    }
}
