using Client.Controller;
using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class VehiclesViewModel : ViewModelBase
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        public List<Vehicle> entryVehicles { get; set; }

        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;

                if (selectedVehicle != null)
                    vehiclesViewController.GetUsersToVehicle(selectedVehicle);

                OnPropertyChanged();
            }
        }

        public ICommand AddEmployeeToVehicle { get; set; }
        public ICommand RemoveEmployeeFromVehicle { get; set; }

        public VehiclesViewController vehiclesViewController;

        private VehicleToEmployeeRelation selectedRelation = new VehicleToEmployeeRelation();

        public VehicleToEmployeeRelation SelectedRelation
        { 
            get => selectedRelation;
            set 
            {
                selectedRelation = value;
                OnPropertyChanged();
            } 
        }

        public ObservableCollection<VehicleToEmployeeRelation> Relations { get; set; } = new ObservableCollection<VehicleToEmployeeRelation>();
    }
}
