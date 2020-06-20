using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Client.Controller
{
    class VehicleController : SubmoduleController
    {
        public ServiceClient socket;
        private VehicleViewModel vViewModel;
        private ContainerViewModel container;

        public VehicleController(ServiceClient socket, ContainerViewModel container)
        {
            this.socket = socket;
            this.container = container;
        }

        public override ViewModelBase Initialize()
        {
            LoadModel();

            container.DeleteCommand = new RelayCommand(ExecuteDeleteVehicleCommand, CanExecuteDeleteVehicleCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            container.NewCommand = new RelayCommand(ExecuteNewCommand);

            return vViewModel;
        }

        void LoadModel()
        {
            int oldvehicleid = 0;
            int oldrelid = 0;
            if (vViewModel != null)
            {
                oldvehicleid = vViewModel.SelectedVehicle.Id;
                oldrelid = vViewModel.SelectedRelation.Id;

            }

            vViewModel = new VehicleViewModel()
            {
                ExecuteAddEmployeeCommand = new RelayCommand(ExecuteDeleteEmployeeCommand, CanExecuteDeleteEmployeeCommand),
                ExecuteDeleteEmployeeCommand = new RelayCommand(ExecuteDeleteEmployeeCommand),
                Vehicles = new ObservableCollection<Vehicle>(socket.GetAllVehicles()),
                SelectedRelations = new ObservableCollection<VehicleToEmployeeRelation>(),
                SelectedVehicle = null,
                SelectedRelation = null,
                Relations = new ObservableCollection<VehicleToEmployeeRelation>(socket.GetAllRelations()),
            };
            if (oldvehicleid != 0)
            {
                vViewModel.SelectedRelations = new ObservableCollection<VehicleToEmployeeRelation>((from rel in vViewModel.Relations where rel.VehicleId.Id == oldvehicleid select rel).ToList().ToList());
                vViewModel.SelectedRelation = vViewModel.SelectedRelations.Single(x => x.Id == oldrelid);
            }

            container.ActiveViewModel = vViewModel;


        }

        public void ExecuteDeleteEmployeeCommand(object obj)
        {
            DialogResult result = (DialogResult)System.Windows.MessageBox.Show("Sind Sie sicher, dass Sie den Mitarbeiter entfernen wollen?", "Relation löschen", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                socket.RemoveRelation(vViewModel.SelectedRelation);
            }
            else
            {
                return;
            }

        }

        public void ExecuteDeleteVehicleCommand(object obj)
        {
            throw new NotImplementedException();
        }

        public void ExecuteNewCommand(object obj)
        {
            var addVeh = new AddVehicleController();
            var emp = addVeh.AddVehicle();

            if (emp != null)
            {
                vViewModel.Vehicles.Add(emp);
            }

        }

        public void ExecuteSaveCommand(object obj)
        {
            throw new NotImplementedException();

            /*

            var databaseEmps = eViewModel.Employees;

            foreach (Employee emp in (from y in databaseEmps where
                                      frontEmps.FirstOrDefault(k => k.Id == y.Id) != null select y))
            {
                socket.EditEmployee(emp);
            }
            
            */
            /*            foreach (Employee emp in databaseEmps)
                        {
                            foreach (Employee front in frontEmps)
                            {
                                if (emp.Id == front.Id)
                                {
                                    socket.EditEmployee(emp);
                                }
                            }
                        }*/

            LoadModel();
        }

        public bool CanExecuteDeleteEmployeeCommand(object obj)
        {
            if (vViewModel.SelectedRelation != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CanExecuteDeleteVehicleCommand(object obj)
        {
            return false;
        }
    }
}
