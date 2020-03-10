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
                    TypeOfSauce = model.TypeOfSauce, //EAC just changed OF to of in model.TypeOfSauce
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
            using (var ctx = new ApplicationDbContext()) //EAC: why redunderlining of ctx here?
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
            //var ctx = new ApplicationDbContext() //EAC: these three lines are showing errors
            //    var x = ctx.Orders[0];
            //var y = x.PizzaCollection[0];

        }

        //Get Pizza by Id - EAC -- EAC stopped here 5pm, switch from ElevenNote template to Pizza stuff
        //public NoteDetail GetNoteById(int id)
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var entity =
        //            ctx
        //                .Notes
        //                .Single(e => e.NoteId == id && e.OwnerId == _userId);
        //        return
        //            new NoteDetail
        //            {
        //                NoteId = entity.NoteId,
        //                Title = entity.Title,
        //                Content = entity.Content,
        //                CreatedUtc = entity.CreatedUtc,
        //                ModifiedUtc = entity.ModifiedUtc
        //            };
        //    }
        }

        //Update/Edit Pizzas -EAC




        //Delete Pizzas -EAC
    }
}
