
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

namespace POS.WebAPI.Controllers
{
    [Authorize]
    public class PizzaController : ApiController
    {
        public IHttpActionResult Get(Employee employee)
        {
            PizzaService pizzaService = CreatePizzaService(employee);
            var pizzas = pizzaService.GetPizzas();
            return Ok(pizzas);
        }
        private PizzaService CreatePizzaService(Employee employee)
        {
            var userId = employee.Id; //Guid.Parse(User.Identity.GetUserId());
            PizzaService pizzaService = new PizzaService(userId);
            return pizzaService;
        }
        public IHttpActionResult Post(PizzaCreate pizza, Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePizzaService(employee);

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
