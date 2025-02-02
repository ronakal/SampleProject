using System;
using System.Linq;
using BusinessEntities;
using System.Net.Http;
using System.Web.Http;
using Core.Services.Products;
using WebApi.Models.Products;
using System.Net;
using Core.Services.Orders;
using Data.Repositories;
using WebApi.Models.Orders;
using System.Collections.Generic;



namespace WebApi.Controllers
{


    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _service;
        private readonly IProductRepository _productRepository; // Needed for adding products to orders

        public OrderController(IOrderService service, IProductRepository productRepository)
        {
            _service = service;
            _productRepository = productRepository;
        }

        [Route("getFilterList")]
        [HttpGet]
        public HttpResponseMessage GetFilterOrders(OrderStatus? status = null, DateTime? startDate = null,DateTime? endDate = null)
        {
            var orders = _service.GetAll(status, startDate, endDate)
                .Where(o =>
                    (!status.HasValue || o.Status == status.Value) &&         
                    (!startDate.HasValue || o.OrderDate >= startDate.Value) && 
                    (!endDate.HasValue || o.OrderDate <= endDate.Value))      
                .Select(o => new OrderModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    Items = o.Items.Select(i => new OrderItemModel
                    {
                        Product = new ProductModel
                        {
                            Id = i.Product.Id,
                            Name = i.Product.Name,
                            StockQuantity = i.Product.StockQuantity,
                            Price = i.Product.Price
                        },
                        Quantity = i.Quantity
                    }).ToList()
                })
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, orders);
        }

        // Get order by ID

        [Route("{id:guid}/getById")]
        [HttpGet]
        public HttpResponseMessage GetOrderById(Guid id)
        {
            var order = _service.GetById(id);
            if (order == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");

            var orderModel = new OrderModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItemModel
                {
                    Product = new ProductModel
                    {
                        Id = i.Product.Id,
                        Name = i.Product.Name,
                        StockQuantity = i.Product.StockQuantity,
                        Price = i.Product.Price
                    },
                    Quantity = i.Quantity
                }).ToList()
            };

            return Request.CreateResponse(HttpStatusCode.OK, orderModel);
        }

        // Create a new order

        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder([FromBody] OrderModel orderModel)
        {
            if (orderModel == null || orderModel.Items == null || orderModel.Items.Count == 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Order must contain at least one item.");

            var order = new Order();
            order.SetOrderDate(orderModel.OrderDate);

            foreach (var item in orderModel.Items)
            {
                var product = _productRepository.GetById(item.Product.Id);

                // If product does not exist, create and add it to the repository
                if (product == null)
                {
                    product = new Product(item.Product.Name, item.Product.Price ?? 0, item.Product.StockQuantity);
                    _productRepository.Create(product);
                }

                try
                {
                    order.AddItem(product, item.Quantity);
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }

            _service.Add(order);

            return Request.CreateResponse(HttpStatusCode.Created, order);
        }

        // Update order status
        [Route("{id:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid id, [FromBody] OrderModel orderModel)
        {
            if (orderModel == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid order data.");

            var existingOrder = _service.GetById(id);
            if (existingOrder == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");

            existingOrder.SetStatus(orderModel.Status);
            existingOrder.SetOrderDate(orderModel.OrderDate);
            bool updated = _service.Update(id, existingOrder);
            return updated ? Request.CreateResponse(HttpStatusCode.OK, existingOrder) : Request.CreateResponse(HttpStatusCode.NotFound);
        }

        // Delete an order
        [Route("{id:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid id)
        {
            bool deleted = _service.Delete(id);
            return deleted ? Request.CreateResponse(HttpStatusCode.OK,"Order deleted") : Request.CreateResponse(HttpStatusCode.NotFound, "Order not found.");
        }
    }


}