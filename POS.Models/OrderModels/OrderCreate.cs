using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.OrderModels
{
    public class OrderCreate
    {
        public int CustomerId { get; set; }
        public int PizzaId { get; set; }
        public bool Delivery { get; set; }
        public double Price { get; set; }

        public ICollection<Pizza> PizzaCollection { get; set; }

    }
}
