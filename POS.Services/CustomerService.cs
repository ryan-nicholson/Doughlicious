using POS.Data;
using POS.Models;
using POS.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class CustomerService
    {

        private readonly int _userId;

        public CustomerService(int userId)
        {
            _userId = userId;
        }

        public bool CreateCustomer(CustomerCreate model)
        {
            var entity = new Customer()
            {
                CustomerId = _userId,
                Name = model.Name,
                EmailAddress = model.EmailAddress,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Customers.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CustomerListItem> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Customers
                        .Where(e => e.CustomerId == _userId)
                        .Select(
                            e =>
                                new CustomerListItem
                                {
                                    CustomerId = e.CustomerId,
                                    Name = e.Name,
                                }
                          );
                return query.ToArray();
            }
        }
        public CustomerDetail GetCustomerById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == id);
                return
                    new CustomerDetail
                    {
                        CustomerId = entity.CustomerId,
                        Name = entity.Name,
                        EmailAddress = entity.EmailAddress,
                        Address = entity.Address,
                        PhoneNumber = entity.PhoneNumber
                    };
            }
        }
        public bool UpdateCustomer(CustomerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == model.CustomerId);
                entity.Name = model.Name;
                entity.EmailAddress = DataType.EmailAddress;
                entity.Address = model.Address;
                entity.PhoneNumber = DataType.PhoneNumber;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCustomer(int CustomerId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Customers
                        .Single(e => e.CustomerId == CustomerId);

                ctx.Customers.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}


