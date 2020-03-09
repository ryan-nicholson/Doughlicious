using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Employee : ApplicationUser //Arthure: see POS.WebAPI/Models/AccountBindingModels EAC: ApplicationUser in POS.Data/IdentityModels.cs is inheriting from IdentityUser, which Peek Definition says comes from metadata
    {
        [Key]
        public int EmployeeId { get; set; } //EAC: a table of only one (this) property seems strange
        public Guid UserId { get; set; }

    }
}
