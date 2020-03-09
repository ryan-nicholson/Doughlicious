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
    public class EmployeeController : ApiController
    {
        public IHttpActionResult Get(ApplicationUser user)
        {
            EmployeeService employeeService = CreateEmployeeService(user);
            var employees = employeeService.GetEmployees(); //This error for GetEmployees() related to GetEmployees() in EmployeeService.cs, which says that function is declared but never used?
            return Ok(employees);
        }

        public IHttpActionResult Post(EmployeeCreate employee, ApplicationUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateEmployeeService(user);

            if (!service.CreateEmployee(employee))//This error for CreateEmployee() related to CreateEmployee() in EmployeeService.cs, which says that function is declared but never used?
                return InternalServerError();

            return Ok();
        }

        private EmployeeService CreateEmployeeService(ApplicationUser applicationUser)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var employeeService = new EmployeeService(userId);
            return employeeService;
        }
    }
}
