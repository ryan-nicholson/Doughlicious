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

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(PizzaCollection))]
        public ICollection<Pizza> PizzaCollection { get; set; }

        public bool Delivery { get; set; }

        public bool Pending { get; set; }

        public DateTime OrderTime { get; }

        public double Price { get; }
    }
}
