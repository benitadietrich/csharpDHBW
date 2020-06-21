using Server.Interfaces;
using Server.Models;
using System.Linq;

namespace Server.Framework
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(string databaseFile) : base(databaseFile)
        {

        }

        public User GetUser(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                try
                {
                    return session.Query<User>()
                          .Where(x => x.Username == username.ToLower()).FirstOrDefault();
                }
                catch
                {
                    return null;
                }

            }
        }

        public bool SetPassword(int userId, string newHash)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        var sessionUser = session.Get<User>(userId);
                        sessionUser.Password = newHash;
                        session.SaveOrUpdate(sessionUser);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }

                }
            }
        }

        public bool UpdateUser(User newUser)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sessionUser = session.Get<User>(newUser.Id);
                    newUser.Password = sessionUser.Password;
                    session.Merge(newUser);
                    transaction.Commit();
                    return true;

                }
            }
        }
    }
}
