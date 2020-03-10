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
        private readonly string _employeeId;

        public OrderService(string employeeId)
        {
            _employeeId = employeeId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var order = new Order()
            {
                EmployeeId = _employeeId,
                CustomerId = model.CustomerId,
                PizzaCollection = model.PizzaCollection,
                Delivery = model.Delivery,
                //Pending = model.Pending,  does it need to be defined when the order is created or only when listed?
                OrderTime = DateTime.Now,
                Price = model.Price
            };

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Orders.Add(order);
                return dbContext.SaveChanges() == 1;
            }
        }

        // Get all orders belnging to employee
        public IEnumerable<OrderListItem> GetAllOrders()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var query = dbContext.Orders
                    .Where(x => x.EmployeeId == _employeeId)
                    .Select(x => new OrderListItem
                    {
                        OrderId = x.OrderId,
                        CustomerId = x.CustomerId,
                        PizzaCollection = x.PizzaCollection,
                        Delivery = x.Delivery,
                        Pending = x.Pending,
                        OrderTime = x.OrderTime,
                        Price = x.Price
                    });

                return query.ToArray();
            }
        }

        public OrderDetail GetOrderById(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Orders
                    .Single(x => x.OrderId == orderId && x.EmployeeId == _employeeId);

                return new OrderDetail
                {
                    OrderId = entity.OrderId,
                    CustomerId = entity.CustomerId,
                    PizzaCollection = entity.PizzaCollection,
                    Delivery = entity.Delivery,
                    Pending = entity.Pending,
                    OrderTime = entity.OrderTime,
                    Price = entity.Price
                };
            }
        }

        public bool UpdateOrder(OrderEdit model)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Orders
                    .Single(x => x.OrderId == model.OrderId && x.EmployeeId == _employeeId);

                entity.PizzaCollection = model.PizzaCollection;
                entity.Delivery = model.Delivery;
                entity.Pending = model.Pending;
                entity.Price = model.Price;

                return dbContext.SaveChanges() == 1;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            // Delete Order from the database
            using (var dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.Orders
                    .Single(x => x.OrderId == orderId && x.EmployeeId == _employeeId);

                dbContext.Orders.Remove(entity);

                return dbContext.SaveChanges() == 1;
            }
        }
    }
}
