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
        public enum CrustType { pan, handtossed, thin }
        public enum ToppingType { pepperoni, sausage, ham, bacon, chicken, mushrooms, onions, tomatoes, blackOlives, bellPeppers, jalapenos, extraCheese }
        public enum SauceType { red, white, pesto }
        public enum SizeType { S, M, L, XL }

        [Key]
        public int PizzaId { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        [Required]//EAC: is this required since it will be autoset?
        public int UserId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public bool Cheese { get; set; }
        [Required]
        public CrustType TypeOfCrust { get; set; }
        [Required]
        public SauceType TypeOfSauce { get; set; }
        [Required]
        public SizeType TypeOfSize { get; set; }

        public ToppingType? TypeOfToppingOne { get; set; }

        public ToppingType? TypeOfToppingTwo { get; set; }

        public ToppingType? TypeOfToppingThree { get; set; }

        public ToppingType? TypeOfToppingFour { get; set; }

        public ToppingType? TypeOfToppingFive { get; set; }

        //EAC: do we make Comment required?
        public string Comment { get; set; }
       //We need to set default value to ""


        private string _comment;
        public string Comment
        {
            get
            {
                return _comment;
            }

            set
            {
                if (value == null)
                {
                    _comment = "";
                }
            }
        }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }

    }


}
