using POS.Data;
using POS.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class EmployeeService //EAC: not sure what should go here, in ElevenNote 4.02 this was mostly about the Note but added Guid and _userId
    {
        private readonly Guid _userId;

        public EmployeeService(Guid userId)
        {
            _userId = userId;


            public bool CreateEmployee(EmployeeCreate model) //EAC: why is there an error on public? Copied from ElevenNote...
            {
                var entity = new Employee()
                    {
                        OwnerId = _userId, //EAC: should OwnerId be EmployeeId? ((Changing to EmployeeId makes it like it even less -- "Cannot implicitly change from type Guid to type int".) Ctrl. suggested fix was to add public Guid OwnerId { get; set;} to POS.Data/Employee.cs

                    CreatedUtc = DateTimeOffset.Now //EAC seems like we would keep this here to show when the employee was created in the system?Ctrl. suggested fix was to add public DateTimeOffset CreatedUtc { get; set;} to POS.Data/Employee.cs

                };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Employees.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }

            public IEnumerable<EmployeeListItem> GetEmployees()//EAC the error on public is that "the local function GetEmployees is declared but never used"  Note from ElevenNote 4.02 says "This method will allow us to see all the notes that belong to a specific user." -- doesn't make sense in that context to exactly replace Note with Employee... change how to just show list of all employees?
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                            .Employees
                            .Where(e => e.OwnerId == _userId)//EAC: should OwnerId be EmployeeId? (Changing to EmployeeId makes it like it even less) Ctrl. suggested fix was to add public Guid OwnerId { get; set;} to POS.Data/Employee.cs
                            .Select(
                                e =>
                                    new EmployeeListItem
                                    {
                                        EmployeeId = e.EmployeeId,
                                        
                                        CreatedUtc = e.CreatedUtc //EAC: Ctrl. suggested fix was to add public DateTimeOffset CreatedUtc { get; set;} to POS.Data/Employee.cs
                                    }
                            );

                    return query.ToArray();
                }
            }

        }


    }
}
