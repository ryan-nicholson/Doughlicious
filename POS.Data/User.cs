using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class POSUser
    {
        public enum UserTypes { Customer, Employee, Manager}

        [Key]
        public int UserId { get; set; }

        public string Name { get; set; }

        public Guid UserGuid { get; set; }

        public UserTypes TypeUser { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
