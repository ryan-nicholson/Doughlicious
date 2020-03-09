
using Microsoft.AspNet.Identity;
using System;
using Microsoft.AspNet.Identity;
using POS.Models.PizzaModels;
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
    public class PizzaController : ApiController
    {
        public IHttpActionResult Get()
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizzas = pizzaService.GetPizzas();
            return Ok(pizzas);
        }
        private PizzaService CreatePizzaService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
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
    }
}
