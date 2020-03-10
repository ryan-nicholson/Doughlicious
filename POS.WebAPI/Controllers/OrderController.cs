using Microsoft.AspNet.Identity;
using POS.Data;
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
        public IHttpActionResult Get(Employee employee)
        {
            OrderService orderService = CreateOrderService(employee);
            var orders = orderService.GetAllOrders();
            return Ok(orders);
        }

        public IHttpActionResult PostOrder(OrderCreate order, Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService(employee);

            if (!service.CreateOrder(order))
                return InternalServerError();

            return Ok();
        }

        private OrderService CreateOrderService(Employee employee)
        {
            var employeeId = employee.Id; //int.Parse(User.Identity.GetUserId());
            var orderService = new OrderService(employeeId);
            return orderService;
        }

        public IHttpActionResult Put(Employee employee, OrderEdit order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateOrderService(employee);

            if (!service.UpdateOrder(order))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(Employee employee, int orderId)
        {
            var service = CreateOrderService(employee);

            if (!service.DeleteOrder(orderId))
                return InternalServerError();

            return Ok();

        }
    }
}
