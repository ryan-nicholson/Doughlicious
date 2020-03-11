using POS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using POS.Models.EmployeeModels;
using POS.Data;

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
        public IHttpActionResult GetUserByEmail(string email)
        {
            UserService userService = CreateUserService();
            var users = userService.GetUserByGuid(email);
            return Ok(users);
        }
        [HttpGet]
        public IHttpActionResult GetUsersByRole(POSUser.UserTypes userTypes)
        {
            if (userTypes == POSUser.UserTypes.Customer)
            {
                UserService userService = CreateUserService();
                var users = userService.GetCustomers();
                return Ok(users);
            }
            if (userTypes == POSUser.UserTypes.Employee)
            {
                UserService userService = CreateUserService();
                var users = userService.GetEmployees();
                return Ok(users);
            }
            if (userTypes == POSUser.UserTypes.Manager)
            {
                UserService userService = CreateUserService();
                var users = userService.GetManagers();
                return Ok(users);
            }
            return BadRequest();
        }
        [HttpPost]
        public IHttpActionResult Post(string email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateUserService();
            var newUser = service.CreatePOSUser(email);

            if (!service.CreatePOSUser(email))
                return InternalServerError();

            return Ok(newUser);
        }
        [HttpPut]
        public IHttpActionResult UpdateUser(UserEdit userEdit)
        {
            if (userTypes == POSUser.UserTypes.Customer)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeCustomer(email);
                return Ok(user);
            }
            if (userTypes == POSUser.UserTypes.Employee)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeEmployee(email);
                return Ok(user);
            }
            if (userTypes == POSUser.UserTypes.Manager)
            {
                UserService userService = CreateUserService();
                var user = userService.ChangeUserTypeManager(email);
                return Ok(user);
            }
            return BadRequest(userTypes.ToString());
        }
        [HttpDelete]
        public IHttpActionResult RemoveUser(string email)
        {
            UserService userService = CreateUserService();
            var user = userService.DeleteUser(email);
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
