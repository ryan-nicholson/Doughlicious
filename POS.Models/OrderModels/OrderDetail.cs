using POS.Data;
using POS.Models.PizzaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.OrderModels
{
    public class OrderDetail
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public List<PizzaListItem> PizzaList { get; set; }
      
        public bool Delivery { get; set; }

        public bool Pending { get; set; }

        public DateTimeOffset OrderTime { get; set; }

        public double Price { get; set; }
    }
}
