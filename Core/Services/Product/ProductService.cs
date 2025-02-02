using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Products
{
    [AutoRegister]
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product CreateProduct(Product product)
        {
            // Add any business logic or validation here
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }            

            return _productRepository.Create(product);
        }

        public bool UpdateProduct(Guid id, Product product)
        {
            // Add any business logic or validation here
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                throw new ArgumentException("Product name cannot be empty.");
            }

            return _productRepository.Update(id, product);
        }

        public bool DeleteProduct(Guid id)
        {
            // Add any business logic or validation here
            return _productRepository.Delete(id);
        }

        public Product GetProductById(Guid id)
        {
            // Add any business logic or validation here
            return _productRepository.GetById(id);
        }

        public List<Product> GetList(string nameFilter = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            // Add any business logic or validation here
            return _productRepository.GetList(nameFilter, minPrice, maxPrice).ToList();
        }
    }
}