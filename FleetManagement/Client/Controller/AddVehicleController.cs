using Client.FleetServiceReference;
using Client.Framework;
using Client.Views;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    public class AddVehicleController
    {
        public AddVehicleView addVehicleWindow;
        public AddVehicleViewModel addVehicleViewModel;

        public Vehicle AddVehicle()
        {
            addVehicleWindow = new AddVehicleView();
            addVehicleViewModel = new AddVehicleViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
            };
            addVehicleWindow.DataContext = addVehicleViewModel;
            return addVehicleWindow.ShowDialog() == true ? addVehicleViewModel.Vehicle : null;
        }

        private void ExecuteAddCommand(object obj)
        {
            addVehicleWindow.DialogResult = true;
            addVehicleWindow.Close();
        }
    }
}
