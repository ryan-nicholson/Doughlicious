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
        public enum CrustType { pan }
        public enum ToppingType { pepperoni }
        public enum SauceType { red }
        public enum SizeType { XXL }

        [Key]
        public int PizzaId { get; set; }

        [ForeignKey(nameof(OrderPizzas))]
        public int OrderId { get; set; }
        public OrderPizzas OrderPizzas { get; set; }

        public int UserId { get; set; }
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
    public class OrderPizzas
    {
        [Key]
        public int OrderPizzaId { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(POSUser))]
        public int UserId { get; set; }
        public virtual POSUser POSUser { get; set; }

        [ForeignKey(nameof(Pizza))]
        public int PizzaId { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
