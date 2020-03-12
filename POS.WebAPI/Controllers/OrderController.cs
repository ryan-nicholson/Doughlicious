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
        public IHttpActionResult Get()
        {
            OrderService orderService = CreateOrderService();
            var orders = orderService.GetAllOrders();
            return Ok(orders);
        }

        public IHttpActionResult PostOrder(OrderCreate order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.CreateOrder(order))
                return InternalServerError();

            return Ok();
        }

        private OrderService CreateOrderService()
        {
            var userGuid = GetUserIdByGuid();
            var orderService = new OrderService(userGuid);
            return orderService;
        }

        public IHttpActionResult Put(OrderEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService();

            if (!service.UpdateOrder(order))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int orderId)
        {
            var service = CreateOrderService();

            if (!service.DeleteOrder(orderId))
                return InternalServerError();

            return Ok();

        }
    }
}
