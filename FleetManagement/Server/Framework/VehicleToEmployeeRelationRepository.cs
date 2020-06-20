using Server.Models;
using System.Collections.Generic;

namespace Server.Framework
{
    public class VehicleToEmployeeRelationRepository : Repository<VehicleToEmployeeRelation>
    {
        public VehicleToEmployeeRelationRepository(string databaseFile) : base(databaseFile)
        {

        }

        public List<VehicleToEmployeeRelation> GetRelationFromVehicle(Vehicle vehicle)
        {
            return this.GetAll().FindAll(x => x.VehicleId.Id == vehicle.Id);
        }

        public List<VehicleToEmployeeRelation> GetRelationFromRelationId(int id)
        {
            return this.GetAll().FindAll(x => x.Id == id);

        }

        public void UpdateRelation(VehicleToEmployeeRelation rel)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Merge(rel);
                    transaction.Commit();
                }
            }
        }
    }
}
