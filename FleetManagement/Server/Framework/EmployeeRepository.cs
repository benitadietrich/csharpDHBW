using Server.Models;
using System.Linq;

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
    }
}

