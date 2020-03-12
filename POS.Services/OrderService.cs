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

        // OrderService Constructor
        public OrderService(int userId)
        {
            _userId = userId;
        }

        public bool CreateOrder(OrderCreate model)
        {
            var order = new Order();
            using (var dbContext = new ApplicationDbContext())
            {
                if (dbContext.UserTable.Find(_userId).TypeUser == POSUser.UserTypes.Customer)
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

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.OrderTable.Add(order);
                return dbContext.SaveChanges() == 1;
            }
        }

        public IEnumerable<OrderListItem> GetAllOrders()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var query = dbContext.OrderTable
                    .Where(x => x.UserId == _userId)
                    .Select(x => new OrderListItem
                    {
                        OrderId = x.OrderId,
                        CustomerId = x.CustomerId,
                        // Do we want to show pizzas?
                        Pizzas = x.Pizzas,
                        Delivery = x.Delivery,
                        Pending = x.Pending,
                        OrderTime = x.OrderTime,
                        Price = x.Price
                    });

                return query.ToArray();
            }
        }

        public OrderDetail1 GetOrderById(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var entity = dbContext.OrderTable
                    .Single(x => x.OrderId == orderId && x.UserId == _userId);

                return new OrderDetail1
                {
                    OrderId = entity.OrderId,
                    CustomerId = entity.CustomerId,
                    Pizzas = entity.Pizzas,
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
                var entity = dbContext.OrderTable
                    .Single(x => x.OrderId == model.OrderId && x.UserId == _userId);

                entity.Pizzas = model.Pizzas;
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
                var entity = dbContext.OrderTable
                    .Single(x => x.OrderId == orderId && x.UserId == _userId);

                dbContext.OrderTable.Remove(entity);

                return dbContext.SaveChanges() == 1;
            }
        }
    }
}

