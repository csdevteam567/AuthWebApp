using AuthWebApp.Models;
using System.Linq;

namespace AuthWebApp
{
    public class UserService
    {
        UsersDbContext usersContext;
        public UserService(UsersDbContext context)
        {
            usersContext = context;
        }

        public Account GetUserById(int id)
        {
            return usersContext.Accounts.Where(a => a.Id == id).First();
        }

        public Account GetUserByLogin(string login)
        {
            return usersContext.Accounts.Where(a => a.Login == login).First();
        }
    }
}
