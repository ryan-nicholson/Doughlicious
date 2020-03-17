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

        [Route("api/Pizza/AllPizzas") ]
        public IHttpActionResult GetPizzas()

        {
            PizzaService pizzaService = CreatePizzaService();
            var pizzas = pizzaService.GetAllPizzas();
            return Ok(pizzas);
        }

        [Route("api/Pizza/GetPizzaByPizzaId")]
        public IHttpActionResult GetPizzaByPizzaId(PizzaDetail pizzaId)
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizza = pizzaService.GetPizzaByPizzaId(pizzaId.PizzaId);
            return Ok(pizza);
        }

        [Route("api/Pizza/GetPizzasByUserId")]
        public IHttpActionResult GetPizzasByUserId()
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizza = pizzaService.GetPizzasByUserId(GetUserIdByGuid());
            return Ok(pizza);
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
        private PizzaService CreatePizzaService()
        {
            var userId = GetUserIdByGuid();
            var pizzaService = new PizzaService(userId);
            return pizzaService;
        }
    }
}
