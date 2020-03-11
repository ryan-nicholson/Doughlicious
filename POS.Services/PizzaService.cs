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
                    //EAC: do we want to include a created and modified time for pizza? or only for order?
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
                var query =
                    ctx
                        .PizzaTable
                        .Where(e => e.UserId == _userId)
                        .Select(
                            e =>
                                new PizzaListItem
                                {//EAC: If this is a list of all pizzas ever, do we want the other pizza properties here, too? Maybe CreatedUtcOffset and maybe ModifiedUtcOffset? And later the Pending status?
                                    PizzaId = e.PizzaId,
                                    CustomerId = e.CustomerId,
                                    OrderId = e.OrderId,
                                    
                                }
                        );

                return query.ToArray();
            }
            //var ctx = new ApplicationDbContext() //EAC: these three lines are showing errors so I commented them out, ask Arthur what they do
            //    var x = ctx.Orders[0];
            //var y = x.PizzaCollection[0];

        }

        //Get Pizza by Id - EAC 
        public PizzaDetail GetPizzaById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Pizzas
                        .Single(e => e.PizzaId == id);//EAC: && e.OwnerId == _userId); -- do we want to restrict who can see it here (e.g. a customer can only see their own pizzas but employee/manager can see everyone's)-- if so, what is the correct OwnerId name now?
                return
                    new PizzaDetail
                    {
                        PizzaId = entity.PizzaId,
                        OrderId = entity.OrderId,
                        EmployeeId = entity.EmployeeId,//EAC: do we need this at all now and what to call it?
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

        //Update/Edit Pizzas -EAC

        public bool UpdatePizza(PizzaEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Pizzas
                    .Single(e => e.PizzaId == model.PizzaId);//EAC: && e.OwnerId == _userId); -- do we want to restrict who can see it here (e.g. a customer can only see their own pizzas but employee/manager can see everyone's)-- if so, what is the correct OwnerId name now?

                entity.PizzaId = model.PizzaId;
                entity.OrderId = model.OrderId;
                entity.EmployeeId = model.EmployeeId;//EAC: do we want to be able to edit EmployeeId in a pizza? I think probably not
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
                entity.ModifiedUtc = DateTimeOffset.UtcNow;//EAC: do we want to have a ModifiedUtcOffset to show pizza edit time? Makes sense to me if only one pizza in an order with multiple pizzas was edited
                return ctx.SaveChanges() == 1;
            }
        }


        //Delete Pizzas -EAC
        public bool DeletePizza(int pizzaId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Pizzas
                    .Single(e => e.PizzaId == pizzaId);//EAC: && e.OwnerId == _userId); -- do we want to restrict who can see it here (e.g. a customer can only see their own pizzas but employee/manager can see everyone's)-- if so, what is the correct OwnerId name now?

                ctx.Pizzas.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
