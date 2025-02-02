using System;
using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product prod) : base(prod)
        {
            Name = prod.Name;    
            StockQuantity = prod.StockQuantity;
            Price = prod.Price;
        }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal? Price { get; set; }
    }
}