
﻿using POS.Data;
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
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        
        public int PizzaId { get; set; }

        public int OrderId { get; set; }
       

        public bool Cheese { get; set; }

        public Pizza.CrustType TypeOfCrust { get; set; }

        public Pizza.SauceType TypeOfSauce { get; set; }

        public Pizza.SizeType TypeOfSize { get; set; }

        public Pizza.ToppingType? TypeOfToppingOne { get; set; }

        public Pizza.ToppingType? TypeOfToppingTwo { get; set; }

        public Pizza.ToppingType? TypeOfToppingThree { get; set; }

        public Pizza.ToppingType? TypeOfToppingFour { get; set; }

        public Pizza.ToppingType? TypeOfToppingFive { get; set; }


        public string Comment { get; set; }//We need to set default value to ""

    }
}
