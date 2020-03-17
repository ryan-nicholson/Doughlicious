using Microsoft.AspNet.Identity;
using POS.Data;
using POS.Models.EmployeeModels;
using POS.Models.OrderModels;
using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POS.WebAPI.Controllers
{
    [Authorize]
    public class OrderController : ApiController
    {
        [HttpGet]
        private int GetUserIdByGuid()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                Guid x = Guid.Parse(User.Identity.GetUserId());
                var query = dbContext.UserTable.Single(e => e.UserGuid == x);
                var userId = query.UserId;
                return userId;
            }
        }

        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            OrderService orderService = CreateOrderService();
            var orders = orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet]
        public IHttpActionResult GetPending()
        {
            var orderService = CreateOrderService();
            var orders = orderService.GetAllPendingOrders();
            return Ok(orders);
        }
        
        [HttpGet]
        public IHttpActionResult GetDelivery()
        {
            var orderService = CreateOrderService();
            var orders = orderService.GetAllDeliveryOrders();
            return Ok(orders);
        }

        [HttpGet]
        public IHttpActionResult GetOrderByOrderId(int orderId)
        {
            var orderService = CreateOrderService();
            var order = orderService.GetOrderById(orderId);
            return Ok(order);
        }

        [HttpGet]
        public IHttpActionResult GetOrderByCustomerId(int customerId)
        {
            var orderService = CreateOrderService();
            var order = orderService.GetOrderByCustomer(customerId);
            return Ok(order);
        }

        [HttpPost]
        public IHttpActionResult PostOrder(OrderCreate order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.CreateOrder(order))
                return InternalServerError();

            return Ok();
        }

        [HttpPost]
        private OrderService CreateOrderService()
        {
            var userGuid = GetUserIdByGuid();
            var orderService = new OrderService(userGuid);
            return orderService;
        }

        [HttpPut]
        public IHttpActionResult EditOrder(OrderEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.UpdateOrder(order))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(OrderDetail orderId)
        {
            var service = CreateOrderService();

            if (!service.DeleteOrder(orderId.OrderId))
                return InternalServerError();

            return Ok();
        }
    }
}
