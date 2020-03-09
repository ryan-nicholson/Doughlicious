using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class CustomerListItem
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}
