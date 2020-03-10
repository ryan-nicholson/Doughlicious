using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.EmployeeModels
{
    public class UserCreate
    {
      
        public Guid UserGuid { get; set; }
        public string Name { get; set; }
        public POSUser.UserTypes TypeUser { get; set; }
    }
}
