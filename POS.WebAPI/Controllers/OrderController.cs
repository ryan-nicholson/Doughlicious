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
        private int GetUserByGuid()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.UserGuid == Guid.Parse(User.Identity.GetUserId()))
                        .Select(
                            e =>
                                new UserListItem
                                {

                                    UserId = e.UserId

                                }
                        ); ;

                return query.ToArray()[0].UserId;
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
            
            var orderService = new OrderService(GetUserByGuid());
            return orderService;
        }
    }
}
