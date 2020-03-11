
using Microsoft.AspNet.Identity;
using System;
using POS.Models.PizzaModels;
using POS.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using POS.Data;
using POS.Models.EmployeeModels;

namespace POS.WebAPI.Controllers
{
    [Authorize]
    public class PizzaController : ApiController
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
            PizzaService pizzaService = CreatePizzaService();
            var pizzas = pizzaService.GetPizzas();
            return Ok(pizzas);
        }
        private PizzaService CreatePizzaService()
        {
            int userId = GetUserByGuid();
            PizzaService pizzaService = new PizzaService(userId);
            return pizzaService;
        }
        public IHttpActionResult Post(PizzaCreate pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePizzaService();

            if (!service.CreatePizza(pizza))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Put(PizzaEdit pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePizzaService(pizza);

            if (!service.UpdatePizza(pizza))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreatePizzaService();

            if (!service.DeletePizza(id))
                return InternalServerError();

            return Ok();
        }
    }
}
