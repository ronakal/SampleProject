using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Extensions;

    public class Order : IdObject
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();
        private OrderStatus _status = OrderStatus.Pending;
        private DateTime _orderDate;

        public DateTime OrderDate
        {
            get => _orderDate;
            private set => _orderDate = value;
        }

        public IEnumerable<OrderItem> Items
        {
            get => _items;
            private set => _items.Initialize(value);
        }

        public OrderStatus Status
        {
            get => _status;
            private set => _status = value;
        }

        public decimal TotalAmount => _items.Sum(item => item.TotalPrice);

        public Order()
        {
            _orderDate = DateTime.Now;
        }

        public void AddItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            product.ReduceStock(quantity);
            _items.Add(new OrderItem(product, quantity));
        }

        public void SetStatus(OrderStatus status)
        {
            _status = status;
        }

        public void SetOrderDate(DateTime orderDate)
        {
            _orderDate = orderDate;
        }
    }

    public class OrderItem
    {
        private Product _product;
        private int _quantity;

        public Product Product
        {
            get => _product;
            private set => _product = value;
        }

        public int Quantity
        {
            get => _quantity;
            private set => _quantity = value;
        }

        public decimal TotalPrice => _product.Price * _quantity;

        public OrderItem(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }
    }

    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }

}
