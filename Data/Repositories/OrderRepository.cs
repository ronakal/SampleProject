using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : IOrderRepository
    {
        private static readonly List<Order> _orders = new List<Order>();
        public List<Order> GetAll(OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return _orders
                .Where(o =>
                    (!status.HasValue || o.Status == status) && // Filter by status
                    (!startDate.HasValue || o.OrderDate >= startDate) && // Filter by start date
                    (!endDate.HasValue || o.OrderDate <= endDate)) // Filter by end date
                .ToList();
        }

        public Order GetById(Guid id) => _orders.FirstOrDefault(o => o.Id == id);

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public bool Update(Guid id, Order updatedOrder)
        {
            var existingOrder = GetById(id);
            if (existingOrder == null) return false;

            existingOrder.SetOrderDate(updatedOrder.OrderDate);
            existingOrder.SetStatus(updatedOrder.Status);
            return true;
        }

        public bool Delete(Guid id)
        {
            var order = GetById(id);
            if (order == null) return false;

            _orders.Remove(order);
            return true;
        }
    }

}
