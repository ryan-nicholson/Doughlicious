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
        [Key]
        public int PizzaId { get; set; }//EAC: -- to discuss with group, probably leave in--remove from PizzaCreate? In ElevenNote, did not include NoteId in NoteCreate because "The id will be created after the POST request happens, after we fill out a form with the [other] properties [listed]... Our .Service and .Data layer will work together to take care of [creating the NoteId]".
        [Required]
        public Pizza.CrustType TypeOfCrust { get; set; }
        [Required]
        public Pizza.SauceType TypeOfSauce { get; set; }
        [Required]
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
