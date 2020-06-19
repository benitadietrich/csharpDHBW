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
    class VehicleViewModel : ViewModelBase
    {
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;
                if (selectedVehicle != null)
                    SelectedRelations = new ObservableCollection<VehicleToEmployeeRelation>((from rel in Relations where rel.VehicleId.Id == selectedVehicle.Id select rel).ToList());
                OnPropertyChanged();
            }
        }

        public ObservableCollection<VehicleToEmployeeRelation> Relations { get; set; }
        public ObservableCollection<VehicleToEmployeeRelation> SelectedRelations { get; set; }
        
        private VehicleToEmployeeRelation selectedRelation;

        public VehicleToEmployeeRelation SelectedRelation
        {
            get => selectedRelation;
            set
            {
                selectedRelation = value;
                OnPropertyChanged();
            }
        }
        public ICommand ExecuteAddEmployeeCommand { get; set; }
        public ICommand ExecuteAddRelationCommand { get; set; }
        public ICommand ExecuteDeleteEmployeeCommand { get; set; }
        public ICommand ExecuteDeleteRelationCommand { get; set; }
    }
}
