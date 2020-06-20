using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModel;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Client.Controller
{
    public class VehiclesViewController : SubmoduleController
    {

        private ServiceClient socket;
        private VehiclesViewModel vehiclesViewModel;
        private ContainerViewModel container;


        public override ViewModelBase Initialize()
        {
            LoadModel();

            container.NewCommand = new RelayCommand(ExecuteNewVehicleCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveVehicleCommand);
            container.DeleteCommand = new RelayCommand(ExecuteDeleteVehicleCommand, CanExecuteDeleteVehCommand);

            return vehiclesViewModel;
        }

        public VehiclesViewController(ServiceClient socket, ContainerViewModel container)
        {
            this.socket = socket;
            this.container = container;
        }

        public void LoadModel()
        {
            vehiclesViewModel = new VehiclesViewModel()
            {
                Vehicles = new ObservableCollection<Vehicle>(socket.GetAllVehicles()),
                entryVehicles = socket.GetAllVehicles().ToList(),
                AddEmployeeToVehicle = new RelayCommand(ExecuteAddEmployeeToVehicleCommand),
                RemoveEmployeeFromVehicle = new RelayCommand(ExecuteRemoveEmployeeFromVehicleCommand, CanExecuteDeleteEmpCommand),
                vehiclesViewController = this,
                SelectedVehicle = null,
            };

            container.ActiveViewModel = vehiclesViewModel;
        }


        public void ExecuteNewVehicleCommand(Object obj)
        {
            AddVehicleController addVehicleController = new AddVehicleController();
            Vehicle vehicle = addVehicleController.AddVehicle();
            if (vehicle == null) return;
            var result = socket.AddVehicle(vehicle);
            if (result == false) System.Windows.MessageBox.Show("Fehler beim Hinzufügen der Fahrzeuge!");
            LoadModel();

        }

        public void ExecuteSaveVehicleCommand(Object obj)
        {
            /*if (vehiclesViewModel.SelectedVehicle != null)
            {
                //If Lists are equaly long
                if (vehiclesViewModel.Vehicles.Count == vehiclesViewModel.entryVehicles.Count)
                {
                    //Change every Input
                    for (int i = 0; i < vehiclesViewModel.Vehicles.Count; i++)
                    {
                        if (vehiclesViewModel.entryVehicles[i] != vehiclesViewModel.Vehicles[i])
                        {
                            var result = socket.ChangeVehicle(vehiclesViewModel.entryVehicles[i], vehiclesViewModel.Vehicles[i]);
                            if (result == false) System.Windows.MessageBox.Show("Fehler beim Ändern der Fahrzeuge!");
                        }
                    }
                    parent.restartVehicleCommand();
                }
            }*/

            var selectedVehicle = vehiclesViewModel.SelectedVehicle;

            if (selectedVehicle != null)
            {
                socket.EditVehicle(selectedVehicle);

            }

            LoadModel();
        }

        public void ExecuteDeleteVehicleCommand(Object obj)
        {
            foreach(VehicleToEmployeeRelation rel in vehiclesViewModel.Relations)
            {
                socket.RemoveRelation(rel);
            }
            var result = socket.RemoveVehicle(vehiclesViewModel.SelectedVehicle);
            if (result == false) System.Windows.MessageBox.Show("Fehler beim Löschen des Fahrzeuges!");

            LoadModel();

        }

        public bool CanExecuteDeleteVehCommand(Object obj)
        {
            return (vehiclesViewModel.SelectedVehicle != null) ? true : false;
        }

        public bool CanExecuteDeleteEmpCommand(object obj)
        {
            return (vehiclesViewModel.SelectedRelation != null) ? true : false;
        }

        private void ExecuteAddEmployeeToVehicleCommand(Object obj)
        {
            if (vehiclesViewModel.SelectedVehicle == null) System.Windows.MessageBox.Show("Bitte wählen Sie ein Fahrzeug.");
            if (vehiclesViewModel.SelectedVehicle != null)
            {
                var relation = new AddRelationController().AddRelation(socket, vehiclesViewModel.SelectedVehicle);
                var result = socket.AddRelation(relation);
                if (result == false) System.Windows.MessageBox.Show("Fehler beim Hinzufügen der Relation");
            }

            LoadModel();
        }

        private void ExecuteRemoveEmployeeFromVehicleCommand(Object obj)
        {
            if (vehiclesViewModel.SelectedRelation != null)
            {
                DialogResult dialogResult = (DialogResult)System.Windows.MessageBox.Show("Sind Sie sicher, dass Sie die Verknüpfung zum ausgewählten Mitarbeiter entfernen wollen?", "Relation Löschen", (MessageBoxButton)MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    var result = socket.RemoveRelation(vehiclesViewModel.SelectedRelation);
                    if (result == false) System.Windows.MessageBox.Show("Fehler beim Löschen der Relation");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            LoadModel();
        }

        public void GetUsersToVehicle(Vehicle vehicle)
        {
            vehiclesViewModel.Relations.Clear();

            var relation = socket.GetRelationFromVehicle(vehicle);

            if (relation != null)
            {
                foreach (var rel in relation)
                {
                    vehiclesViewModel.Relations.Add(rel);
                }
            }
            

        } 
    }
}
