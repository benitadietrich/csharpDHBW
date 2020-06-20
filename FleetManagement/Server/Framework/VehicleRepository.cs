using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Framework
{
    class VehicleRepository : Repository<Vehicle>
    {
        public VehicleRepository(string databaseFile) : base(databaseFile)
        {

        }

        public bool IsVehicleReferredToEmp(Employee emp)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                try
                {
                    if (session.Query<VehicleToEmployeeRelation>()
                          .Where(x => x.EmployeeId.Id == emp.Id).FirstOrDefault() != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch { return false; }


            }
        }

        public void UpdateVehicle(Vehicle veh)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Merge(veh);
                    transaction.Commit();
                }
            }
        }
    }
}
