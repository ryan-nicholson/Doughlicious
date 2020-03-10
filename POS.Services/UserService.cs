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




        public bool CreatePOSUser( string email)
        {
            var entity = new POSUser()
            {
                UserGuid = GetGuid(email),
                TypeUser = POSUser.UserTypes.Customer,
                Name = GetNameByGuid(email)
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserTable.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<POSUser> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx.UserTable;

                return query.ToArray();
            }
        }
        private string GetNameByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.UserTable.Find(email);
                
                return user.Name;
            }
        }
        public Guid GetGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.UserTable.Find(email);

                return user.UserGuid;
            }
        }
        public UserListItem GetUserByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var user = ctx.UserTable.Find(email);
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.UserGuid == user.UserGuid)
                        .Select(
                            e =>
                            new UserListItem
                            {

                                UserId = e.UserId,
                                Name = e.Name,
                                UserGuid = user.UserGuid

                            }
                            
                        ) ; 

                return query.ToArray()[user.UserId];
            }
        }
        public IEnumerable<UserListItem> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.UserGuid == _userGuid && e.TypeUser == POSUser.UserTypes.Customer)
                        .Select(
                            e =>
                                new UserListItem
                                {
                                    UserId = e.UserId,
                                    UserGuid = e.UserGuid,
                                    Name = e.Name
                                }
                        ); ;

                return query.ToArray();
            }
        }
        public IEnumerable<UserListItem> GetEmployees()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.UserGuid == _userGuid && e.TypeUser == POSUser.UserTypes.Employee)
                        .Select(
                            e =>
                                new UserListItem
                                {
                                    UserId = e.UserId,
                                    UserGuid = e.UserGuid,
                                    Name = e.Name
                                }
                        ); ;

                return query.ToArray();
            }
        }
        public POSUser ChangeUserTypeEmployee(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Find(_userGuid);
                
                var query = ctx.UserTable.Find(email);

                if (usertype.TypeUser == POSUser.UserTypes.Manager)
                {
                    query.TypeUser = POSUser.UserTypes.Employee;
                }

                return query;
            }
        }
        public POSUser ChangeUserTypeCustomer(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Find(_userGuid);

                var query = ctx.UserTable.Find(email);

                if (usertype.TypeUser == POSUser.UserTypes.Manager)
                {
                    query.TypeUser = POSUser.UserTypes.Employee;
                }

                return query;
            }
        }
        public POSUser ChangeUserTypeManager(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Find(_userGuid);

                var query = ctx.UserTable.Find(email);

                if (usertype.TypeUser == POSUser.UserTypes.Manager)
                {
                    query.TypeUser = POSUser.UserTypes.Manager;
                }

                return query;
            }
        }
        public bool DeleteUser(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Find(_userGuid);

                var query = ctx.UserTable.Find(email);

                if (usertype.TypeUser == POSUser.UserTypes.Manager)
                {
                    ctx.UserTable.Remove(query);
                }
                return usertype.TypeUser == POSUser.UserTypes.Manager;


            }
        }
    }


}

