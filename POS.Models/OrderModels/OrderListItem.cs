using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.OrderModels
{
    public class OrderListItem : OrderCreate
    {
        public int OrderId { get; set; }
        public bool Pending { get; set; }
        public DateTime OrderTime { get; set; }

    }
}
