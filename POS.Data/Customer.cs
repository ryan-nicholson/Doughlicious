using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public object EmailAddress { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public object PhoneNumber { get; set; }

        [ForeignKey(nameof(CustomerOrderCollection))]
        public ICollection<Order> CustomerOrderCollection { get; set; }

    }
}
