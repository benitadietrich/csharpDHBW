using Client.FleetServiceReference;
using Client.Framework;
using Client.Models;
using Client.ViewModels;
using MaterialDesignColors.Recommended;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class CostsBusinessUnitController : SubmoduleController
    {
        private ServiceClient socket;
        private ContainerViewModel container;
        private CostsBusinessUnitViewModel viewModel;

        public CostsBusinessUnitController(ServiceClient socket, ContainerViewModel container)
        {
            this.socket = socket;
            this.container = container;
        }

        public override ViewModelBase Initialize()
        {

            viewModel = new CostsBusinessUnitViewModel()
            {
                Costs = GetData()
            };

            container.DeleteCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.SaveCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.NewCommand = new RelayCommand(EmptyCommand, DisabledCommand);

            return viewModel;
        }

        public IEnumerable<CostsBusinessUnitModel> GetData()
        {

            var vehicles = socket.GetAllVehicles();
            var employees = socket.GetAllEmployees();
            var businessUnits = socket.GetAllBusinessUnits();

            if (vehicles.Count == 0)
                return null;

            var relations = socket.GetAllRelations();

            var result = businessUnits
                .Join(employees, b => b.Id, e => e.BusinessUnitId.Id, (b, e) => new { BusinessUnit = b, Employee = e })
                .Join(relations, be => be.Employee.Id, ve => ve.EmployeeId.Id, (be, ve) => new { BusinessUnitEmployee = be, VehicleEmployee = ve })
                .Join(vehicles, beve => beve.VehicleEmployee.VehicleId.Id, v => v.Id, (beve, v) => new { beve.BusinessUnitEmployee.BusinessUnit, beve.VehicleEmployee, Vehicle = v })
                .Select(m => new { m.BusinessUnit, Costs = GetCostsPerVehicle(m.VehicleEmployee, m.Vehicle) })
                .SelectMany(bv => bv.Costs.Select(c => new { VehicleCost = c, bv.BusinessUnit }))
                .GroupBy(cb => new { cb.VehicleCost.Month, cb.BusinessUnit })
                .Select(cb => new CostsBusinessUnitModel { Month = cb.Key.Month, BusinessUnit = cb.Key.BusinessUnit, Costs = cb.Sum(c => c.VehicleCost.Costs) });

            return result;
        }

        private IEnumerable<(DateTime Month, int Count, decimal Costs)> GetCostsPerVehicle(VehicleToEmployeeRelation vehicleEmployee, Vehicle vehicle)
        {
            for (DateTime i = vehicleEmployee.StartDate; i < (vehicleEmployee.EndDate ?? DateTime.Now); i = i.AddMonths(1))
            {
                yield return (new DateTime(i.Year, i.Month, 1), 1, Convert.ToDecimal(vehicle.Insurance / 12 + vehicle.LeasingRate));
            }
        }

        public void EmptyCommand(object obj)
        {

        }

        public bool DisabledCommand(object obj)
        {
            return false;
        }
    }
}
