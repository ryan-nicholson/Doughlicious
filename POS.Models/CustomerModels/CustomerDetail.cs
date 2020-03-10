using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.CustomerModels
{
    public class CustomerDetail
    {
        public int CustomerId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public object EmailAddress { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public object PhoneNumber { get; set; }
    }
}
