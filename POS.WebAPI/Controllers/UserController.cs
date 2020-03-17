using POS.Services;
using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using POS.Data;
using static POS.Data.POSUser;
using POS.Models.UserModels;
using POS.Models.EmployeeModels;


namespace POS.WebAPI.Controllers
{
    [Authorize]

    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/User/Get")]
        public IHttpActionResult Get()
        {
            UserService userService = CreateUserService();
            var users = userService.GetUsers();
            return Ok(users);
        }
        [HttpGet]
        [Route("api/User/GetUserByEmail")]
        public IHttpActionResult GetUserByEmail(UserGetEmail email)
        {
            UserService userService = CreateUserService();
            var users = userService.GetUserByGuid(email.Email);
            return Ok(users);
        }
        [HttpGet]
        [Route("api/User/GetUsersByRole")]
        public IHttpActionResult GetUsersByRole(UserGetUserType userTypes)
        {
            if (ModelState.IsValid)
            {
                UserService userService = CreateUserService();
                var users = userService.GetUsersByRole(userTypes.typeUser);
                return Ok(users);
            }
            return BadRequest();
        }
        [HttpPost]

        public IHttpActionResult Post(UserCreate model)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserService();

            var newUser = service.CreatePOSUser(model.Email);

            if (!service.CreatePOSUser(model.Email))

                return InternalServerError();

            return Ok(newUser);
        }
        [HttpPut]
        public IHttpActionResult UpdateUser(UserEdit userEdit)
        {

            if (ModelState.IsValid)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserType(userEdit.Email, userEdit.userType);
                return Ok(user);
            }

            return BadRequest();
        }
        [HttpDelete]
        public IHttpActionResult RemoveUser(UserDelete delete)
        {
            UserService userService = CreateUserService();
            var user = userService.DeleteUser(delete.Email);

            if (user)
            {
                return Ok();
            }
            return BadRequest();
        }
        private UserService CreateUserService()
        {
            var userGuid = Guid.Parse(User.Identity.GetUserId());
            var employeeService = new UserService(userGuid);
            return employeeService;
        }
    }
}
