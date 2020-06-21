using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IVehicleToEmployeeRelation : IRepository<VehicleToEmployeeRelation>
    {
        List<VehicleToEmployeeRelation> GetRelationFromVehicle(Vehicle vehicle);

        List<VehicleToEmployeeRelation> GetRelationFromRelationId(int id);

        void UpdateRelation(VehicleToEmployeeRelation rel);
        
    }
}
