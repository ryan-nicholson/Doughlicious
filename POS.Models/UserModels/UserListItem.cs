using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EmployeeModels
{
    public class UserListItem
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
