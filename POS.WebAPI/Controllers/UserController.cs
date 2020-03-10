using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;//EAC manually typed this in with a hint from ElevenNote 4.03 step 7
using POS.Models.EmployeeModels;
using POS.Data;

namespace POS.WebAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public IHttpActionResult Get()
        {
            UserService userService = CreateUserService();
            var users = userService.GetUserByGuid();
            return Ok(users);
        }

        public IHttpActionResult Post(UserCreate user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserService();

            if (!service.CreatePOSUser(user))
                return InternalServerError();

            return Ok();
        }

        private UserService CreateUserService()
        {
            var userGuid = Guid.Parse(User.Identity.GetUserId());
            var employeeService = new UserService(userGuid);
            return employeeService;
        }
    }
}
