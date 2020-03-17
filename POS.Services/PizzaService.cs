
using POS.Data;
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
        private readonly int _userId;

        public PizzaService(int userId)
        {
            _userId = userId;
        }
        
        public bool CreatePizza(PizzaCreate model)
        {
            var entity =
                new Pizza()
                {
                    UserId = _userId,
                    OrderId = model.OrderId,
                    CustomerId = model.CustomerId,
                    Cheese = model.Cheese,
                    Comment = model.Comment,
                    TypeOfCrust = model.TypeOfCrust,
                    TypeOfSauce = model.TypeOfSauce,
                    TypeOfSize = model.TypeOfSize,
                    TypeOfToppingOne = model.TypeOfToppingOne,
                    TypeOfToppingTwo = model.TypeOfToppingTwo,
                    TypeOfToppingThree = model.TypeOfToppingThree,
                    TypeOfToppingFour = model.TypeOfToppingFour,
                    TypeOfToppingFive = model.TypeOfToppingFive,
                    CreatedUtc = DateTimeOffset.UtcNow
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.PizzaTable.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<PizzaListItem> GetPizzas()

        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.PizzaTable
                    .Select(e => new PizzaListItem
                    {
                        UserId = e.UserId,
                        PizzaId = e.PizzaId,
                        CustomerId = e.CustomerId,
                        OrderId = e.OrderId,
                        CreatedUtc = e.CreatedUtc
                    });

                return query.ToArray();
            }

        }

        //Get Pizza by Id - EAC 
        public Pizza GetPizzaByPizzaId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PizzaTable
                        .Single(e => e.PizzaId == id);
                return
                    new Pizza
                    {
                        PizzaId = entity.PizzaId,
                        OrderId = entity.OrderId,
                        UserId = entity.UserId,//EAC: do we need this at all now and what to call it?
                        CustomerId = entity.CustomerId,//EAC same note as EmployeeId
                        Cheese = entity.Cheese,
                        TypeOfCrust = entity.TypeOfCrust,
                        TypeOfSauce = entity.TypeOfSauce,
                        TypeOfSize = entity.TypeOfSize,
                        TypeOfToppingOne = entity.TypeOfToppingOne,
                        TypeOfToppingTwo = entity.TypeOfToppingTwo,
                        TypeOfToppingThree = entity.TypeOfToppingThree,
                        TypeOfToppingFour = entity.TypeOfToppingFour,
                        TypeOfToppingFive = entity.TypeOfToppingFive,
                        Comment = entity.Comment
                    };
            }
        }
        //Get Pizza by User - EAC
        public IEnumerable<Pizza> GetPizzasByUserId(int userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .PizzaTable
                        .Where(e => e.UserId == userId)
                        .Select(e => new Pizza
                        {
                            PizzaId = e.PizzaId,
                            OrderId = e.OrderId,
                            UserId = e.UserId,
                            CustomerId = e.CustomerId,
                            Cheese = e.Cheese,
                            TypeOfCrust = e.TypeOfCrust,
                            TypeOfSauce = e.TypeOfSauce,
                            TypeOfSize = e.TypeOfSize,
                            TypeOfToppingOne = e.TypeOfToppingOne,
                            TypeOfToppingTwo = e.TypeOfToppingTwo,
                            TypeOfToppingThree = e.TypeOfToppingThree,
                            TypeOfToppingFour = e.TypeOfToppingFour,
                            TypeOfToppingFive = e.TypeOfToppingFive,
                            Comment = e.Comment
                        });
                return entity;
            }
        }

        //Update/Edit Pizzas -EAC

        public bool UpdatePizza(PizzaEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PizzaTable
                    .Single(e => e.PizzaId == model.PizzaId);

                entity.PizzaId = model.PizzaId;
                entity.OrderId = model.OrderId;
                entity.UserId = model.UserId;//EAC: do we want to be able to edit EmployeeId in a pizza? I think probably not
                entity.CustomerId = model.CustomerId;//EAC same note as EmployeeId
                entity.Cheese = model.Cheese;
                entity.TypeOfCrust = model.TypeOfCrust;
                entity.TypeOfSauce = model.TypeOfSauce;
                entity.TypeOfSize = model.TypeOfSize;
                entity.TypeOfToppingOne = model.TypeOfToppingOne;
                entity.TypeOfToppingTwo = model.TypeOfToppingTwo;
                entity.TypeOfToppingThree = model.TypeOfToppingThree;
                entity.TypeOfToppingFour = model.TypeOfToppingFour;
                entity.TypeOfToppingFive = model.TypeOfToppingFive;
                entity.Comment = model.Comment;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }

        //Delete Pizzas -EAC
        public bool DeletePizza(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .PizzaTable
                    .Single(e => e.PizzaId == id);

                ctx.PizzaTable.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
