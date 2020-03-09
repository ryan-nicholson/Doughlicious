using Microsoft.AspNet.Identity;
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
            int employeeId = int.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(employeeId);
            return orderService;
        }
    }
}
