using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Products;

namespace WebApi.Models.Orders
{
    public class OrderItemModel
    {
        public ProductModel Product { get; set; } // Now contains ProductModel
        public int Quantity { get; set; }
        public decimal TotalPrice => (Product?.Price ?? 0) * Quantity;
    }

}