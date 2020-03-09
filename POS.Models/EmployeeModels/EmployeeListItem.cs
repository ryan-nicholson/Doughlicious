using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EmployeeModels
{
    public class EmployeeListItem
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }//EAC added, seems like a list of employees would include their name -- or do we just add : ApplicationUser to the class line? How to show only some of the fields/properties from ApplicationUser but not all of them?
        //[Display (Name="Created")]
        //public DateTimeOffset Created { get; set:} //EAC: may want this property to show how long they've been working here? Same note as Name about including here or getting from ApplicationUser
    }
}
