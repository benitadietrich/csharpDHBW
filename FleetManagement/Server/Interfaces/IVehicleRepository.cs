using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {

        bool IsVehicleReferredToEmp(Employee emp);

        void UpdateVehicle(Vehicle veh);

        Vehicle GetVehicle(int id);

        Vehicle GetByLicense(string license);
    }
}
