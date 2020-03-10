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
                    TypeOfSauce = model.TypeOFSauce,
                    TypeOfSize = model.TypeOfSize,
                    TypeOfToppingOne = model.TypeOfToppingOne,
                    TypeOfToppingTwo = model.TypeOfToppingTwo,
                    TypeOfToppingThree = model.TypeOfToppingThree,
                    TypeOfToppingFour = model.TypeOfToppingFour,
                    TypeOfToppingFive = model.TypeOfToppingFive
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
