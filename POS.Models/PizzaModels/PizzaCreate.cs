using POS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.PizzaModels
{
    public class PizzaCreate
    {
        
        public Pizza.CrustType TypeOfCrust { get; set; }
        public Pizza.SauceType TypeOfSauce { get; set; }
        public Pizza.SizeType TypeOfSize { get; set; }
        
        public Pizza.ToppingType? TypeOfToppingOne { get; set; }//EAC: just added ?s after .ToppingType because toppings are optional
        public Pizza.ToppingType? TypeOfToppingTwo { get; set; }
        public Pizza.ToppingType? TypeOfToppingThree { get; set; }
        public Pizza.ToppingType? TypeOfToppingFour { get; set; }
        public Pizza.ToppingType? TypeOfToppingFive { get; set; }
        public bool Cheese { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
    }
}
