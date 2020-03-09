using POS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Employee:ApplicationUser //see POS.WebAPI/Models/AccountBindingModels
    {
        [Key]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeOrderCollection))]
        public ICollection<Order> EmployeeOrderCollection { get; set; }

    }
}
