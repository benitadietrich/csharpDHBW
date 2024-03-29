﻿using Client.Controller;
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
                    controller.GetUsersToVehicle(selectedVehicle);

                OnPropertyChanged();
            }
        }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand RemoveEmployeeCommand { get; set; }

        public VehiclesViewController controller;

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
