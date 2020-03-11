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
        public IHttpActionResult Get()
        {
            UserService userService = CreateUserService();
            var users = userService.GetUsers();
            return Ok(users);
        }
        [HttpGet]

        public IHttpActionResult GetUserByEmail(UserGetEmail email)
        {
            UserService userService = CreateUserService();
            var users = userService.GetUserByGuid(email.Email);
            return Ok(users);
        }
        [HttpGet]
        public IHttpActionResult GetUsersByRole(UserGetUserType userTypes)
        {
            if (userTypes.typeUser == POSUser.UserTypes.Customer)

            {
                UserService userService = CreateUserService();
                var users = userService.GetCustomers();
                return Ok(users);
            }

            if (userTypes.typeUser == POSUser.UserTypes.Employee)

            {
                UserService userService = CreateUserService();
                var users = userService.GetEmployees();
                return Ok(users);
            }

            if (userTypes.typeUser == POSUser.UserTypes.Manager)

            {
                UserService userService = CreateUserService();
                var users = userService.GetManagers();
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

            if (userEdit.userType == POSUser.UserTypes.Customer)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeCustomer(userEdit.Email);
                return Ok(user);
            }
            if (userEdit.userType == POSUser.UserTypes.Employee)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeEmployee(userEdit.Email);
                return Ok(user);
            }
            if (userEdit.userType == POSUser.UserTypes.Manager)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeManager(userEdit.Email);
                return Ok(user);
            }
            return BadRequest(userEdit.userType.ToString());
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
