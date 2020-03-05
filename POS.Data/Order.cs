using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }

        public virtual Pizza Pizza { get; set; }

        public bool Delivery { get; set; }

        public DateTime OrderTime { get; }

        public double Price { get; }
    }
}
