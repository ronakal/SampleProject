using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IProductRepository
    {
        Product Create(Product product);
        bool Update(Guid id, Product updatedProduct);
        bool Delete(Guid id);
        Product GetById(Guid id);
        List<Product> GetList(string name = null, decimal? minPrice = null, decimal? maxPrice = null);
    }
}
