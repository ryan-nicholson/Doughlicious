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
    public class OrderController : ApiController
    {
        [HttpGet]
        private int GetUserIdByGuid()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var query = dbContext.UserTable
                   .Find(Guid.Parse(User.Identity.GetUserId()));
                var userId = query.UserId;

                return userId;
            }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            OrderService orderService = CreateOrderService();
            var orders = orderService.GetAllOrders();
            return Ok(orders);
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
