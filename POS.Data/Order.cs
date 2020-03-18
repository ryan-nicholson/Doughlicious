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


        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual POSUser User { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();

        [Required]
        public bool Delivery { get; set; }

        public bool Pending { get; set; }

        [Required]
        public DateTimeOffset OrderTime { get; set; }

        public DateTimeOffset? ModifiedOrderTime { get; set; }


        private double _total = 0;

        [Required]
        public double Price
        {
            get
            {
                foreach (var pizza in Pizzas)
                {
                    _total += pizza.Price;
                }
                return _total;
            }
            set
            {
                //if (Pizzas != null)
                //{



                // }

                //_total = value;
            }
        }
    }
}

