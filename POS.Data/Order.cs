using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Employee))]
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey(nameof(PizzaCollection))]
        public ICollection<Pizza> PizzaCollection { get; set; }

        public bool Delivery { get; set; }

        public bool Pending { get; set; }

        // Added a set property to OrderTime, couldn't set the order time for the order when being created
        public DateTime OrderTime { get; set; }

        // Added a set property to Price, couldn't set the price for the order when being created
        public double Price { get; set; }
    }
}

