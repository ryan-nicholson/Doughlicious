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
        public virtual POSUser User { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }

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
                return _total;
            }
            set
            {
                if (Pizzas != null)
                {
                    
                    foreach (var pizza in Pizzas)
                    {
                        _total += pizza.Price;
                    }
                    
                }

                //_total = value;
            }
        }
    }
}

