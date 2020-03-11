using POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models.UserModels
{
    public class UserEdit
    {
        public string Email { get; set; }
        public POSUser.UserTypes userType { get; set; }
    }
}
