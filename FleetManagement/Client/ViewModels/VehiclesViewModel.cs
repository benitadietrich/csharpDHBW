using Client.FleetServiceReference;
using Client.Framework;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    class VehiclesViewModel : ViewModelBase
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        private Vehicle selectedVehicle;

        public Vehicle SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<VehicleToEmployeeRelation>

    }
}
