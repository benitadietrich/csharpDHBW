using Server.Models;

namespace Server.Framework
{
    class VehicleToEmployeeRelationRepository : Repository<VehicleToEmployeeRelation>
    {
        public VehicleToEmployeeRelationRepository(string databaseFile) : base(databaseFile)
        {

        }
    }
}
