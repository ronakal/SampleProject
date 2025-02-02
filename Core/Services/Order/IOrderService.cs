using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IOrderService
    {
        List<Order> GetAll(OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null);
        Order GetById(Guid id);
        void Add(Order order);
        bool Update(Guid id, Order updatedOrder);
        bool Delete(Guid id);
    }
}
