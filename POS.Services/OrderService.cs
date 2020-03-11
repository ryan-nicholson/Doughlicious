using POS.Data;
using POS.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class OrderService
    {
        // Creating a field to tie the order to a reference object (employee making order)
        private readonly int _userId;

        public OrderService(int userId)
        {
            _userId = userId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var order = new Order();
            using (var ctx = new ApplicationDbContext())

            {


                if (ctx.UserTable.Find(_userId).TypeUser == POSUser.UserTypes.Customer)
                {

                    {
                        order.UserId = _userId;
                        order.CustomerId = _userId;
                        order.Delivery = model.Delivery;
                        order.Pending = true;
                        order.OrderTime = DateTime.Now;
                        order.Price = model.Price;
                    };
                }
                else
                {

                    {

                        order.UserId = _userId;
                        order.CustomerId = model.CustomerId;
                        order.Delivery = model.Delivery;
                        order.Pending = true;
                        order.OrderTime = DateTime.Now;
                        order.Price = model.Price;

                    };

                }
            };

            var dbContext = new ApplicationDbContext();

            dbContext.OrderTable.Add(order);
            return dbContext.SaveChanges() == 1;

        }


        public IEnumerable<OrderListItem> GetAllOrders()
        {
            var dbContext = new ApplicationDbContext();

            var query = dbContext.OrderTable
                .Where(x => x.UserId == _userId)
                .Select(x => new OrderListItem
                {
                    OrderId = x.OrderId,
                    UserId = x.UserId,
                    Delivery = x.Delivery,
                    Pending = x.Pending,
                    OrderTime = x.OrderTime,
                    Price = x.Price
                });

            return query.ToArray();
        }
    }
}

