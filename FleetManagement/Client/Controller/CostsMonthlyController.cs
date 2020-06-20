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
    class CostsMonthlyController : SubmoduleController
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
                Costs = new ObservableCollection<CostsMonthlyModel>(GetData())
            };

            container.DeleteCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.SaveCommand = new RelayCommand(EmptyCommand, DisabledCommand);
            container.NewCommand = new RelayCommand(EmptyCommand, DisabledCommand);

            return viewModel;
        }

        private List<CostsMonthlyModel> GetData()
        {
            var res = new List<CostsMonthlyModel>();
            var veh = socket.GetAllVehicles();

            veh.ForEach(vehicle =>
            {
                for (var i = vehicle.LeasingFrom; i < vehicle.LeasingTo; i = i.AddMonths(1))
                {
                    res.Add(new CostsMonthlyModel()
                    {
                        Month = string.Format("{0} {1}", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i.Month), i.Year),
                        Ammount = 1,
                        Costs = Convert.ToDecimal(vehicle.LeasingRate + (vehicle.Insurance / 12))
                    });
                }
            });

            return res.GroupBy(x => x.Month).Select(y => new CostsMonthlyModel() 
            { Month = y.Key, Ammount = y.Count(), Costs = y.Sum(z => z.Costs), CostDisplay = string.Format("€ {0}", y.Sum(z => z.Costs).ToString("0.00")) }).OrderByDescending(z => z.Costs).ToList();

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
