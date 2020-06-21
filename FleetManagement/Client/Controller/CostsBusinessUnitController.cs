using Client.FleetServiceReference;
using Client.Framework;
using Client.Models;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var result = from b in businessUnits
                         join e in employees on b.Id equals e.BusinessUnitId.Id
                         join be in relations on e.Id equals be.EmployeeId.Id
                         join beve in vehicles on be.VehicleId.Id equals beve.Id
                         select new { BusinessUnit = b, Costs = GetCostsPerVehicle(be, beve) };

            var res2 = from y in result.SelectMany(bv => bv.Costs.Select(c => new { VehicleCost = c, BUnit = bv.BusinessUnit }))
                       group y by new { Month = y.VehicleCost.Month, Unit = y.BUnit } into g
                       select new CostsBusinessUnitModel { Month = string.Format("{0} {1}", g.Key.Month.Year, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month.Month)), BusinessUnit = g.Key.Unit, CostsDisplay = string.Format("€ {0}", Convert.ToDecimal(g.Sum(c => c.VehicleCost.Costs)).ToString("0.00")) };

            return res2;
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
