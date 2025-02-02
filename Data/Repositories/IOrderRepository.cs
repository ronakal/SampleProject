using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAll(OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null);
        Order GetById(Guid id);
        void Add(Order order);
        bool Update(Guid id, Order updatedOrder);
        bool Delete(Guid id);
    }

}
