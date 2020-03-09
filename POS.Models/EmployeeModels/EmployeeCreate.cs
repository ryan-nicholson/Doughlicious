using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EmployeeModels
{
    public class EmployeeCreate
    {
       //EAC: would add [Required] here and any length requirements if not already handled elsewhere (ApplicationUser?
        public int EmployeeId { get; set; }//EAC --changed from Name to EmployeeId ; do we need this here or do we use the :ApplicationUser in the class line? ElevenNote Module 4.01 says not to put Id here as that will be created after the POST request happens, with .Service and .Data layers working together to take care of that.
    }
}
