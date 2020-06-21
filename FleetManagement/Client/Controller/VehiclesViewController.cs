using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System;
using System.Collections.ObjectModel;
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
                AddEmployeeCommand = new RelayCommand(ExecuteAddRelationCommand),
                RemoveEmployeeCommand = new RelayCommand(ExecuteRemoveEmployeeFromVehicleCommand, CanExecuteDeleteEmpCommand),
                controller = this,
                SelectedVehicle = null,
            };

            container.ActiveViewModel = vehiclesViewModel;
        }


        public void ExecuteNewVehicleCommand(object obj)
        {
            AddVehicleController addVehicleController = new AddVehicleController();
            Vehicle vehicle = addVehicleController.AddVehicle();
            if (vehicle == null || vehicle.LicensePlate == null || vehicle.LicensePlate == "" || vehicle.LeasingRate == 0 || vehicle.Insurance == 0 || vehicle.Model == "" || vehicle.Model == null)
                return;
            else
            {
                if (socket.GetAllVehicles().Find(x => x.LicensePlate == vehicle.LicensePlate.ToLower().Replace(" ", "-")) != null)
                    System.Windows.Forms.MessageBox.Show("Ein Fahrzeug mit diesem Kennzeichen existiert bereits", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (!socket.AddVehicle(vehicle))
                        System.Windows.Forms.MessageBox.Show("Fehler beim Hinzufügen des Fahrzeugs", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        System.Windows.Forms.MessageBox.Show("Fahrzeug wurde erfolgreich hinzugefügt", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            LoadModel();

        }

        public void ExecuteSaveVehicleCommand(object obj)
        {

            if (vehiclesViewModel.SelectedVehicle != null)
            {
                if (!socket.EditVehicle(vehiclesViewModel.SelectedVehicle))
                {
                    System.Windows.Forms.MessageBox.Show("Die Daten konnten nicht gepseichert werden, da Sie eventuell von einem anderen Benutzer bereits bearbeitet wurden.", "Speicherfehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
            }

            LoadModel();
        }

        public void ExecuteDeleteVehicleCommand(object obj)
        {
            foreach (VehicleToEmployeeRelation rel in vehiclesViewModel.Relations)
            {
                socket.RemoveRelation(rel);
            }
            var result = socket.RemoveVehicle(vehiclesViewModel.SelectedVehicle);
            if (result == false)
                System.Windows.Forms.MessageBox.Show("Fehler beim Löschen des Fahrzeuges. Versuchen Sie es noch einmal.", "Fehler beim Löschen des Fahrzeugs", MessageBoxButtons.OK, MessageBoxIcon.Error);

            LoadModel();

        }

        public bool CanExecuteDeleteVehCommand(object obj)
        {
            return (vehiclesViewModel.SelectedVehicle != null) ? true : false;
        }

        public bool CanExecuteDeleteEmpCommand(object obj)
        {
            return (vehiclesViewModel.SelectedRelation != null) ? true : false;
        }

        private void ExecuteAddRelationCommand(object obj)
        {
            if (vehiclesViewModel.SelectedVehicle == null)
                System.Windows.Forms.MessageBox.Show("Bitte wählen Sie ein Fahrzeug.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            else
            {
                new AddRelationController(socket, vehiclesViewModel.SelectedVehicle).AddRelation();
            }

            LoadModel();
        }

        private void ExecuteRemoveEmployeeFromVehicleCommand(Object obj)
        {
            if (vehiclesViewModel.SelectedRelation != null)
            {
                DialogResult dialogResult = (DialogResult)System.Windows.MessageBox.Show("Sind Sie sicher, dass Sie die Verknüpfung zum ausgewählten Mitarbeiter entfernen wollen?", "Relation Löschen", (MessageBoxButton)MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var result = socket.RemoveRelation(vehiclesViewModel.SelectedRelation);
                    if (result == false)
                        System.Windows.Forms.MessageBox.Show("Fehler beim Löschen der Relation", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
