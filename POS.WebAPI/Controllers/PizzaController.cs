using Microsoft.AspNet.Identity;
using POS.Data;
using POS.Models.EmployeeModels;
using POS.Models.PizzaModels;
using POS.Services;
using System;
using System.Linq;
using System.Web.Http;

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
                        .Single(e => e.UserGuid == Guid.Parse(User.Identity.GetUserId()));

                return query.UserId;

            }
        }
        [HttpGet]
        public IHttpActionResult Get(string getall)
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizzas = pizzaService.GetPizzas();
            return Ok(pizzas);
        }

        [HttpGet]
        public IHttpActionResult GetPizzaByPizzaId(PizzaDetail pizzaId, string getbyid)
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizza = pizzaService.GetPizzaByPizzaId(pizzaId.PizzaId);
            return Ok(pizza);
        }

        [HttpGet]
        public IHttpActionResult GetPizzasByUserId(string getbyuser)
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizza = pizzaService.GetPizzasByUserId(GetUserByGuid());
            return Ok(pizza);
        }

        private PizzaService CreatePizzaService()
        {
            int userId = GetUserByGuid();
            PizzaService pizzaService = new PizzaService(userId);
            return pizzaService;
        }

        [HttpPost]
        public IHttpActionResult Post(PizzaCreate pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePizzaService();

            if (!service.CreatePizza(pizza))
                return InternalServerError();

            return Ok();
        }
        [HttpPut]
        public IHttpActionResult EditPizza(PizzaEdit pizza)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePizzaService();

            if (!service.UpdatePizza(pizza))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(PizzaDetail id)
        {
            var service = CreatePizzaService();

            if (!service.DeletePizza(id.PizzaId))
                return InternalServerError();

            return Ok();
        }

    }
}
