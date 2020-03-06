using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class EmployeeListItem
    {
        public int EmployeeId { get; set; }
        public int Name { get; set; } //EAC: change to EmployeeName?
        //public DateTimeOffset EmployeeCreatedDate { get; set; } //EAC: would this be useful?
    }
}
