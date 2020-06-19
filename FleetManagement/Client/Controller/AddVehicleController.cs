using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller
{
    class AddVehicleController
    {
        private AddVehicleView view;
        private AddVehicleViewModel viewModel;
        private ServiceClient socket;
        private Vehicle vehicle;

        public Vehicle AddVehicle(ServiceClient socket)
        {
            this.socket = socket;
            view = new AddVehicleView();
            viewModel = new AddVehicleViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddVehicleCommand)
            };

            view.DataContext = viewModel;

            return view.ShowDialog() == true ? vehicle : null;
        }

        public void ExecuteAddVehicleCommand(object obj)
        {
            vehicle = new Vehicle()
            {
                Brand = viewModel.Brand,
                Insurance = viewModel.Insurance,
                LeasingFrom = viewModel.LeasingFrom,
                LeasingTo = viewModel.LeasingTo,
                LeasingRate = viewModel.LeasingRate,
                LicensePlate = viewModel.LicensePlate,
                Model = viewModel.Model
            };

            try
            {
                if (socket.AddVehicle(vehicle))
                {
                    MessageBox.Show("Fahrzeug wurde erfolgreich hinzugefügt");
                    view.DialogResult = true;
                    view.Close();
                }
                else
                {
                    MessageBox.Show("Ein Fahrzeug mit diesem Kennzeichen existiert bereits");
                }


            }
            catch
            {
                MessageBox.Show("Ein Fehler ist aufgetreten überprüfen Sie ihre Eingaben");
            }

        }
    }
}
