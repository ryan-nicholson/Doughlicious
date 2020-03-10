using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.PizzaModels
{
    public class PizzaListItem
    {
        public int PizzaId { get; set; }
        //public Pizza Pizza { get; set; } //EAC: we think we don't need this
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }

    }
}
