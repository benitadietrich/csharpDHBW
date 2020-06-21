using Server.Interfaces;
using Server.Models;
using System.Linq;

namespace Server.Framework
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
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

        public Vehicle GetVehicle(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                try
                {
                    return session.Query<Vehicle>()
                          .Where(x => x.Id == id).FirstOrDefault();
                }
                catch
                {
                    return null;
                }

            }
        }

        public Vehicle GetByLicense(string license)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                try
                {
                    return session.Query<Vehicle>()
                      .Where(x => x.LicensePlate == license).FirstOrDefault();
                }
                catch
                {
                    return null;
                }

            }

        }
    }
}
