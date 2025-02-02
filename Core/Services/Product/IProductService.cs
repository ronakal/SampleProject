using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Products
{
    public interface IProductService
    {
        List<Product> GetList(string name = null, decimal? minPrice = null, decimal? maxPrice = null);
        Product GetProductById(Guid id);
        Product CreateProduct(Product product);
        bool UpdateProduct(Guid id, Product updatedProduct);
        bool DeleteProduct(Guid id);
    }
}