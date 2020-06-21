using Client.FleetServiceReference;
using Client.Framework;
using Client.Models;
using Client.ViewModels;
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
    public class CostsMonthlyController : SubmoduleController
    {
        ServiceClient socket;
        ContainerViewModel container;
        CostsMonthlyViewModel viewModel;

        public CostsMonthlyController(ServiceClient socket, ContainerViewModel container)
        {
            this.socket = socket;
            this.container = container;
        }


        public override ViewModelBase Initialize()
        {

            viewModel = new CostsMonthlyViewModel()
            {
                Costs = GetData()
            };

            container.DeleteCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.SaveCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.NewCommand = new RelayCommand(EmptyCommand, DisabledCommand);

            return viewModel;
        }

        private Dictionary<string, CostsMonthlyModel> GetData()
        {
            var res = new List<CostsMonthlyModel>();
            var veh = socket.GetAllVehicles();
            if (veh.Count() == 0)
                return null;

            var min = veh.Min(v => v.LeasingFrom);
            var max = veh.Max(v => v.LeasingTo);

            Dictionary<string, CostsMonthlyModel> costsMonthly = new Dictionary<string, CostsMonthlyModel>();

            for (var i = min; i < max; i = i.AddMonths(1))
            {
                var vehm = veh.Where(v => v.LeasingFrom <= i && v.LeasingTo >= i);
                var costs = Convert.ToDecimal(vehm.Select(v => v.LeasingRate + v.Insurance / 12).Sum());
                var x = new CostsMonthlyModel()
                {
                    Ammount = vehm.Count(),
                    Costs = costs,
                    CostDisplay = string.Format("€ {0}", costs.ToString("0.00"))
                };
                costsMonthly.Add(string.Format("{0} {1}",i.Year, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i.Month)), x);
            }

            return costsMonthly;

        }



        private bool DisabledCommand(object obj)
        {
            return false;
        }

        private void EmptyCommand(object obj)
        {

        }
    }
}
