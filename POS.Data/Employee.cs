using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Employee : ApplicationUser //see POS.WebAPI/Models/AccountBindingModels
    {
        [Key]
        public int EmployeeId { get; set; }
    }
}
