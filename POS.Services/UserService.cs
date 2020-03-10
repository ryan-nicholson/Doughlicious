using Microsoft.VisualBasic.ApplicationServices;
using POS.Data;
using POS.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Services
{
    public class UserService
    {
        private readonly Guid _userGuid;

        public UserService(Guid userGuid)
        {
            _userGuid = userGuid;
        }

        


        public bool CreatePOSUser(UserCreate model)
        {
            var entity = new POSUser()
            {
                UserGuid = _userGuid,
                TypeUser = model.TypeUser,
                Name = GetNameByGuid()
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserTable.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<UserListItem> GetUserByGuid()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users
                        .Where(e => Guid.Parse(e.Id) == _userGuid)
                        .Select(
                            e =>
                                new UserListItem
                                {
                                    
                                    UserGuid = Guid.Parse(e.Id)

                                }
                        ); ;

                return query.ToArray();
            }
        }
        private string GetNameByGuid()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users
                        .Where(e => Guid.Parse(e.Id) == _userGuid)
                        .Select(
                            e =>
                                new UserListItem
                                {

                                    Name = e.Name

                                }
                        ); ;

                return query.ToArray()[0].Name;
            }
        }
    }


}

