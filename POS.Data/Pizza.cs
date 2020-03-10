using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data
{
    public class Pizza
    {
        public enum CrustType { pan }//EAC: confirm with group: "pan, handtossed, thin" ?
        public enum ToppingType { pepperoni }//EAC: confirm with group: "pepperoni, sausage, ham, bacon, chicken, mushrooms, onions, tomatoes, black olives, bell peppers, jalapenos (any way to include the tilde?), extra cheese"?
        public enum SauceType { red }//EAC: confirm with group: "red, white, pesto"?
        public enum SizeType { XXL }//EAC: confirm with group: "small, medium, large, extra large"?

        [Key]
        public int PizzaId { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }


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

        public string Comment { get; set; }//We need to set default value to ""

    }


}
