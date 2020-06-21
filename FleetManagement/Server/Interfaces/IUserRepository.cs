using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string username);

        bool SetPassword(int userId, string newHash);

        bool UpdateUser(User newUser);
    }
}
