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

        [HttpGet]
        public IHttpActionResult GetPizzaByPizzaId(PizzaDetail pizzaId)
        {
            PizzaService pizzaService = CreatePizzaService();
            var pizza = pizzaService.GetPizzaByPizzaId(pizzaId.PizzaId);
            return Ok(pizza);
        }
        [HttpGet]
        public IHttpActionResult GetPizzasByUserId()
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

        public IHttpActionResult Delete(PizzaDetail id)
        {
            var service = CreatePizzaService();

            if (!service.DeletePizza(id.PizzaId))
                return InternalServerError();

            return Ok();
        }
        private UserListItem GetUserByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var user = ctx.UserTable.Find(email);
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.UserGuid == user.UserGuid)
                        .Select(
                            e =>
                            new UserListItem
                            {

                                UserId = e.UserId,
                                Name = e.Name,
                                UserGuid = user.UserGuid

                            }

                        );

                return query.ToArray()[user.UserId];
            }
        }
    }
}
