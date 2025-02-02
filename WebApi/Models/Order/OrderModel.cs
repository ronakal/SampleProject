using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemModel> Items { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount => Items?.Sum(i => i.TotalPrice) ?? 0;
    }

}