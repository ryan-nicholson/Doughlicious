using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.PizzaModels
{
    public class PizzaEdit
    {
        public enum CrustType { pan }
        public enum ToppingType { pepperoni }
        public enum SauceType { red }
        public enum SizeType { XXL }
        public int PizzaId { get; set; }

        public int OrderId { get; set; }
        


        public string EmployeeId { get; set; }
        public int CustomerId { get; set; }

        public bool Cheese { get; set; }

        public CrustType TypeOfCrust { get; set; }

        public SauceType TypeOfSauce { get; set; }

        public SizeType TypeOfSize { get; set; }

        public ToppingType? TypeOfToppingOne { get; set; }

        public ToppingType? TypeOfToppingTwo { get; set; }

        public ToppingType? TypeOfToppingThree { get; set; }

        public ToppingType? TypeOfToppingFour { get; set; }

        public ToppingType? TypeOfToppingFive { get; set; }

        public string Comment { get; set; }
    }
}
