




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

        public bool CreatePOSUser(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {

                int x = 0;
                foreach (var query in ctx.UserTable)
                {
                    if (query.Email == email)
                    {
                        x += 1;
                    }
                }

                if (x == 0)
                {
                    var entity = new POSUser()
                    {
                        UserGuid = GetGuid(email),
                        TypeUser = POSUser.UserTypes.Customer,
                        Name = GetNameByGuid(email),
                        Email = GetEmailByGuid(email)
                    };
                    {
                        ctx.UserTable.Add(entity);
                        return ctx.SaveChanges() == 1;
                    }
                }
                return false;
            }
        }

        public IEnumerable<UserListItem> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<UserListItem> userList = new List<UserListItem>();
                var query = ctx.UserTable.Select(e => new UserListItem
                {
                    UserGuid = e.UserGuid,
                    UserId = e.UserId,
                    Name = e.Name,
                    Email = e.Email,
                });
                return query.ToList();
            }
        }

        private string GetNameByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Single(e => e.Email == email);
                return user.Name;
            }
        }
        private string GetEmailByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Single(e => e.Email == email);
                return user.Email;
            }
        }
        public Guid GetGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var user = ctx.Users.Single(e => e.Email == email);


                return Guid.Parse(user.Id);
            }
        }
        public UserListItem GetUserByGuid(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var user = ctx.UserTable.Single(e => e.Email == email);
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
                                UserGuid = user.UserGuid,
                                Email = e.Email
                                
                            }
                            
                        ) ; 

                return query.ToArray()[user.UserId -1];
            }
        }
        public IEnumerable<UserListItem> GetUsersByRole( POSUser.UserTypes type)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.TypeUser == type)
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
        public IEnumerable<UserListItem> GetCustomers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.TypeUser == POSUser.UserTypes.Customer)
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
                        .Where(e => e.TypeUser == POSUser.UserTypes.Employee)
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
        public IEnumerable<UserListItem> GetManagers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .UserTable
                        .Where(e => e.TypeUser == POSUser.UserTypes.Manager)
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
        public POSUser ChangeUserType(string email, POSUser.UserTypes type)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Single(e => e.UserGuid == _userGuid).TypeUser;
                
                var query = ctx.UserTable.Single(e=> e.Email == email);

                if (usertype == POSUser.UserTypes.Manager)
                {
                    query.TypeUser = type;
                    ctx.SaveChanges();
                }

                return query;
            }
        }
        public POSUser ChangeUserTypeCustomer(string email)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var usertype = ctx.UserTable.Single(e => e.UserGuid == _userGuid).TypeUser;

                var query = ctx.UserTable.Single(e => e.Email == email);

                if (usertype == POSUser.UserTypes.Manager)
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
                var usertype = ctx.UserTable.Single(e => e.UserGuid == _userGuid).TypeUser;

                var query = ctx.UserTable.Single(e => e.Email == email);

                if (usertype == POSUser.UserTypes.Manager)
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
                var usertype = ctx.UserTable.Single(e => e.UserGuid == _userGuid).TypeUser;

                var query = ctx.UserTable.Single(e => e.Email == email);

                if (usertype == POSUser.UserTypes.Manager)
                {
                    ctx.UserTable.Remove(query);
                    ctx.SaveChanges();
                }
                return usertype == POSUser.UserTypes.Manager;
            }
        }
    }
}

