using Server.Models;
using System.Linq;

namespace Server.Framework
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(string databaseFile) : base(databaseFile)
        {

        }

        public User GetUser(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Query<User>()
                      .Where(x => x.Username == username).FirstOrDefault();

            }
        }
    }
}
