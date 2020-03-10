using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class User
    {
        public enum UserTypes { Customer, Employee, Manager}
        [Key]
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public UserTypes TypeUser { get; set; }
        public ICollection<Order> UserOrders { get; set; }
    }
}
