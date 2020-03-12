using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.OrderModels
{
    public class OrderEdit
    {
        public int OrderId { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }

        public bool Delivery { get; set; }

        public bool Pending { get; set; }

        public double Price { get; set; }
    }
}
