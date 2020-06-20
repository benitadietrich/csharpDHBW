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

        public Dictionary<DateTime, CostsBusinessUnitModel> GetData()
        {

            var res = new List<CostsMonthlyModel>();
            var veh = socket.GetAllVehicles();
            var rels = socket.GetAllRelations();
            var emps = socket.GetAllEmployees();
            if (veh.Count() == 0) return null;
            var min = veh.Min(v => v.LeasingFrom);
            var max = veh.Max(v => v.LeasingTo);

            Dictionary<DateTime, CostsBusinessUnitModel> costsMonthly = new Dictionary<DateTime, CostsBusinessUnitModel>();

            for (var i = min; i < max; i = i.AddMonths(1))
            {
                var vehm = veh.Where(v => v.LeasingFrom <= i && v.LeasingTo >= i && rels.FirstOrDefault(rel => rel.VehicleId.Id == v.Id) != null);
                var bus = (from emp in emps where rels.FirstOrDefault(rel => rel.EmployeeId.Id == emp.Id && vehm.FirstOrDefault(v => v.Id == rel.VehicleId.Id) != null) != null select emp.BusinessUnitId).ToList();
                var costs = Convert.ToDecimal(vehm.Select(v => v.LeasingRate + v.Insurance / 12).Sum());
                var temp = new Dictionary<DateTime, Dictionary<BusinessUnit, decimal>>();

                foreach(BusinessUnit bu in bus)
                {
                    var y = new Dictionary<BusinessUnit, decimal>();
                    y.Add(bu, Convert.ToDecimal((from v in vehm where rels.FirstOrDefault(r => r.EmployeeId.BusinessUnitId.Id == bu.Id && r.VehicleId.Id == v.Id) != null select (v.LeasingRate + v.Insurance / 12)).Sum()));
                    temp.Add(i, y);
                }

                var x = new CostsBusinessUnitModel()
                {
                    BusCosts = temp
                };
                costsMonthly.Add(i, x);
            }

            return costsMonthly;

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
