﻿using POS.Data;
using POS.Models.PizzaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class PizzaService
    {
        private readonly string _userId;

        //private readonly int _userId;

        public PizzaService(string userId)
        {
            _userId = userId;
        }
        //public PizzaService(int userId)
        //{
        //    _userId = userId;
        //}
        public bool CreatePizza(PizzaCreate model)
        {
            var entity =
                new Pizza()
                {
                    EmployeeId = _userId,
                    CustomerId = model.CustomerId,
                    OrderId = model.OrderId,
                    Cheese= model.Cheese,
                    Comment = model.Comment,
                    TypeOfCrust = model.TypeOfCrust,
                    TypeOfSauce = model.TypeOfSauce,
                    TypeOfSize = model.TypeOfSize,
                    TypeOfToppingOne = model.TypeOfToppingOne,
                    TypeOfToppingTwo = model.TypeOfToppingTwo,
                    TypeOfToppingThree = model.TypeOfToppingThree,
                    TypeOfToppingFour = model.TypeOfToppingFour,
                    TypeOfToppingFive = model.TypeOfToppingFive
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Pizzas.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<PizzaListItem> GetPizzas()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Pizzas
                        .Where(e => e.EmployeeId == _userId)
                        .Select(
                            e =>
                                new PizzaListItem
                                {
                                    PizzaId = e.PizzaId,
                                    CustomerId = e.CustomerId,
                                    OrderId = e.OrderId,
                                    
                                }
                        );

                return query.ToArray();
            }
        }
    }
}
