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
        private readonly int _employeeId;

        public OrderService(int employeeId)
        {
            _employeeId = employeeId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var order = new Order()
            {
                EmployeeId = _employeeId,
                CustomerId = model.CustomerId,
                PizzaId = model.PizzaId,
                Delivery = model.Delivery,
                //Pending = model.Pending,  does it need to be defined when the order is created or only when listed?
                OrderTime = DateTime.Now,
                Price = model.Price
            };

            var dbContext = new ApplicationDbContext();

            dbContext.Orders.Add(order);
            return dbContext.SaveChanges() == 1;

        }

        // Get all orders belnging to employee
        public IEnumerable<OrderListItem> GetAllOrders()
        {
            var dbContext = new ApplicationDbContext();

            var query = dbContext.Orders
                .Where(x => x.EmployeeId == _employeeId)
                .Select(x => new OrderListItem
                {
                    OrderId = x.OrderId,
                    CustomerId = x.CustomerId,
                    PizzaId = x.PizzaId,
                    Delivery = x.Delivery,
                    Pending = x.Pending,
                    OrderTime = x.OrderTime,
                    Price = x.Price
                });

            return query.ToArray();
        }
    }
}
