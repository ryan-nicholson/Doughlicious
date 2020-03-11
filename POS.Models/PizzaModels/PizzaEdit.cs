using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POS.Data.Pizza;

namespace POS.Models.PizzaModels
{
    public class PizzaEdit
    {
        public int PizzaId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }


        public string EmployeeId { get; set; }//EAC: do we want to be able to edit EmployeeId in a pizza? I think probably not
        public int CustomerId { get; set; }//EAC: do we want to be able to edit CustomerId in a pizza? I think probably not

        public bool Cheese { get; set; }

        public CrustType TypeOfCrust { get; set; }

        public SauceType TypeOfSauce { get; set; }

        public SizeType TypeOfSize { get; set; }

        public ToppingType? TypeOfToppingOne { get; set; }

        public ToppingType? TypeOfToppingTwo { get; set; }

        public ToppingType? TypeOfToppingThree { get; set; }

        public ToppingType? TypeOfToppingFour { get; set; }

        public ToppingType? TypeOfToppingFive { get; set; }

        public string Comment { get; set; }//We need to set default value to ""
    }
}
