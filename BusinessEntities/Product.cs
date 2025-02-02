using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    using System;
    using System.Collections.Generic;
    using Common.Extensions;

    public class Product : IdObject
    {
        private string _name;
        private decimal _price;
        private int _stockQuantity;
        public Product(string name, decimal price, int stockQuantity)
        {
            SetName(name);
            SetPrice(price);
            SetStockQuantity(stockQuantity);
        }

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public int StockQuantity
        {
            get => _stockQuantity;
            private set => _stockQuantity = value;
        }


        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Product name was not provided.");
            }
            _name = name;
        }

        public void SetPrice(decimal price)
        {
            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }
            _price = price;
        }

        public void SetStockQuantity(int stockQuantity)
        {
            if (stockQuantity < 0)
            {
                throw new ArgumentException("Stock quantity cannot be negative.");
            }
            _stockQuantity = stockQuantity;
        }

        public void ReduceStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
            if (_stockQuantity < quantity)
            {
                throw new InvalidOperationException("Not enough stock available.");
            }
            _stockQuantity -= quantity;
        }
    }


}
