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

        public OrderService(int employeeId)
        {
            _userId = employeeId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var order = new Order()
            {
                UserId = _userId,
                CustomerId = model.CustomerId,
                PizzaCollection = model.PizzaCollection,
                Delivery = model.Delivery,
                Pending = true,
                OrderTime = DateTime.Now,
                Price = model.Price
            };

            var dbContext = new ApplicationDbContext();

            dbContext.OrderTable.Add(order);
            return dbContext.SaveChanges() == 1;

        }

        // Get all orders belnging to employee
        public IEnumerable<OrderListItem> GetAllOrders()
        {
            var dbContext = new ApplicationDbContext();

            var query = dbContext.OrderTable
                .Where(x => x.UserId == _userId)
                .Select(x => new OrderListItem
                {
                    OrderId = x.OrderId,
                    UserId = x.UserId,
                    PizzaCollection = x.PizzaCollection,
                    Delivery = x.Delivery,
                    Pending = x.Pending,
                    OrderTime = x.OrderTime,
                    Price = x.Price
                });

            return query.ToArray();
        }
    }
}
