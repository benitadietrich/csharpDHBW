using Server.Models;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Server.Framework
{
    class EmployeeRepository : Repository<Employee>
    {
        public EmployeeRepository(string databaseFile) : base(databaseFile)
        {

        }

        public bool IsEmployeeReferred(BusinessUnit businessUnit)
        {
            using (var session = NHibernateHelper.OpenSession())
            {

                if (session.Query<Employee>()
                      .Where(x => x.BusinessUnitId.Id == businessUnit.Id).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public bool UpdateEmployee(Employee emp)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Merge(emp);
                    transaction.Commit();
                    return true;

                }
            }
        }

        public Employee GetEmployee(int id)
        {
            using(var session = NHibernateHelper.OpenSession())
            {
                using ( var transaction = session.BeginTransaction())
                {
                    try
                    {
                        return session.Query<Employee>()
                               .Where(x => x.Id == id).FirstOrDefault();

                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public Employee GetEmployeeByNumber(int number)
        {
            using(var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        return session.Query<Employee>()
                            .Where(x => x.EmployeeNumber == number).FirstOrDefault();
                    } catch
                    {
                        return null;
                    }
                }
            }
        }
    }
}

