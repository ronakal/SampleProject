using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using Data.Repositories;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public List<Order> GetAll(OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null) =>
            _repository.GetAll(status, startDate, endDate);

        public Order GetById(Guid id) => _repository.GetById(id);

        public void Add(Order order) => _repository.Add(order);

        public bool Update(Guid id, Order updatedOrder) => _repository.Update(id, updatedOrder);

        public bool Delete(Guid id) => _repository.Delete(id);
    }

}
