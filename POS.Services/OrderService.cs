using POS.Data;
using POS.Models.OrderModels;
using POS.Models.PizzaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class OrderService
    {
        private readonly POSUser user = new POSUser();

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
                        order.OrderTime = DateTimeOffset.Now;
                        //order.Price = model.Price;
                    };
                }
                else
                {
                    {
                        order.UserId = _userId;
                        order.CustomerId = model.CustomerId;
                        order.Delivery = model.Delivery;
                        order.Pending = true;
                        order.OrderTime = DateTimeOffset.Now;
                        //order.Price = model.Price;
                    };
                }
            };

            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.OrderTable.Add(order);
                return dbContext.SaveChanges() == 1;
            }
        }

        // Get all orders for user unless manager
        public IEnumerable<OrderListItem> GetAllOrders()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    // If user is a manager then they will be displayed all orders that exist, showing the UserId for the user that placed the order
                    var query = dbContext.OrderTable
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            UserId = x.UserId,
                            CustomerId = x.CustomerId,
                            Delivery = x.Delivery,
                            Pending = x.Pending,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }
                else
                {
                    // If user is an employee or customer they will be displayed all delivery orders that they have placed
                    var query = dbContext.OrderTable
                        .Where(x => x.UserId == _userId)
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            CustomerId = x.CustomerId,
                            Delivery = x.Delivery,
                            Pending = x.Pending,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }

            }
        }

        /*
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
                          //Pizzas = x.Pizzas,
                          Delivery = x.Delivery,
                          Pending = x.Pending,
                          OrderTime = x.OrderTime,
                          Price = x.Price
                      });

                return query.ToArray();
            }
        }
        */

        // Get all pending orders for user unless manager
        public IEnumerable<OrderListItem> GetAllPendingOrders()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    // If user is a manager then they will be displayed all pending orders that exist, showing the UserId for the user that placed the order
                    var query = dbContext.OrderTable
                        .Where(x => x.Pending == true)
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            UserId = x.UserId,
                            CustomerId = x.CustomerId,
                            Delivery = x.Delivery,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }
                else
                {
                    // If user is an employee or customer they will be displayed all pending orders that they have placed
                    var query = dbContext.OrderTable
                        .Where(x => x.UserId == _userId && x.Pending == true)
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            CustomerId = x.CustomerId,
                            Delivery = x.Delivery,
                            //Pending = x.Pending,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }
            }
        }

        // Get all delivery orders for user unless manager
        public IEnumerable<OrderListItem> GetAllDeliveryOrders()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    // If user is a manager then they will be displayed all delivery orders that exist, showing the UserId for the user that placed the order
                    var query = dbContext.OrderTable
                        .Where(x => x.Delivery == true)
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            UserId = x.UserId,
                            CustomerId = x.CustomerId,
                            Pending = x.Pending,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }
                else
                {
                    // If user is an employee or customer they will be displayed all delivery orders that they have placed
                    var query = dbContext.OrderTable
                        .Where(x => x.UserId == _userId && x.Delivery == true)
                        .Select(x => new OrderListItem
                        {
                            OrderId = x.OrderId,
                            CustomerId = x.CustomerId,
                            //Delivery = x.Delivery,
                            Pending = x.Pending,
                            OrderTime = x.OrderTime,
                            Price = x.Price
                        });

                    return query.ToArray();
                }
            }
        }
        
        public OrderDetail GetOrderByOrderId(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == orderId);

                    return new OrderDetail
                    {
                        // If user is a manager they will be displayed the requested order's detailed information along with who took the order
                        OrderId = entity.OrderId,
                        UserId = entity.UserId,
                        CustomerId = entity.CustomerId,
                        //Pizzas = entity.Pizzas,
                        Delivery = entity.Delivery,
                        Pending = entity.Pending,
                        OrderTime = entity.OrderTime,
                        Price = entity.Price
                    };
                }
                else
                {
                    // If user is a customer or employee they will be displayed the requested order's detailed information only if it is an order they took
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == orderId && x.UserId == _userId);

                    // Create PizzaListItem
                    //var pizzaList = new PizzaListItem();
                    var pizzaList = new List<PizzaListItem>();

                    foreach (var pizza in entity.Pizzas)
                    {
                        PizzaListItem orderPizza = new PizzaListItem()
                        {
                            PizzaId = pizza.PizzaId

                        };

                        pizzaList.Add(orderPizza);
                    }

                    var order = new OrderDetail
                    {
                        OrderId = entity.OrderId,
                        CustomerId = entity.CustomerId,
                        //Pizzas = entity.Pizzas,
                        PizzaList = pizzaList,
                        Delivery = entity.Delivery,
                        Pending = entity.Pending,
                        OrderTime = entity.OrderTime,
                        Price = entity.Price
                    };

                    return order;
                }
            }
        }

        public IEnumerable<OrderListItem> GetOrdersByCustomerId(int customerId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var query = dbContext.OrderTable
                    .Where(x => x.CustomerId == customerId)
                    .Select(x => new OrderListItem
                    {
                        OrderId = x.OrderId,
                        UserId = x.UserId,
                        CustomerId = x.CustomerId,
                        Pending = x.Pending,
                        OrderTime = x.OrderTime,
                        Price = x.Price
                    });

                   return query.ToArray();
            }
        }
           
        public bool UpdateOrder(OrderEdit model)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    // If user is a manager they can update any requested order
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == model.OrderId);

                    entity.Pizzas = model.Pizzas;
                    entity.Delivery = model.Delivery;
                    entity.Pending = model.Pending;
                    entity.Price = model.Price;
                    entity.ModifiedOrderTime = DateTimeOffset.Now;

                    return dbContext.SaveChanges() == 1;
                }
                else
                {
                    // If user is a customer or employee they can only update a requested order they took
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == model.OrderId && x.UserId == _userId);

                    entity.Pizzas = model.Pizzas;
                    entity.Delivery = model.Delivery;
                    entity.Pending = model.Pending;
                    entity.Price = model.Price;
                    entity.ModifiedOrderTime = DateTimeOffset.Now;

                    return dbContext.SaveChanges() == 1;
                }
            }
        }

        public bool DeleteOrder(int orderId)
        {
            // Delete Order from the database
            using (var dbContext = new ApplicationDbContext())
            {
                if (user.TypeUser == POSUser.UserTypes.Manager)
                {
                    // If user is a manager they can delete any requested order
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == orderId);

                    dbContext.OrderTable.Remove(entity);

                    return dbContext.SaveChanges() == 1;
                }
                else
                {
                    // If user is a customer or employee they can only delete a requested order they took
                    var entity = dbContext.OrderTable
                        .Single(x => x.OrderId == orderId && x.UserId == _userId);

                    dbContext.OrderTable.Remove(entity);

                    return dbContext.SaveChanges() == 1;
                }
            }
        }
    }
}

