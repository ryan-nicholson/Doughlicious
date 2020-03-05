using POS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class Employee:ApplicationUser
    {
        [Key]
        public int EmployeeId { get; set; }

        public Guid GlobalId { get; }

        public string Name { get; set; }

    }
}
