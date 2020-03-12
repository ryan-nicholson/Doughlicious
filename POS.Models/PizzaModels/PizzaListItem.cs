using POS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.PizzaModels
{
    public class PizzaListItem
    {
        public int PizzaId { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }

        public int OrderId { get; set; }

        [Display(Name="Created")]
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
