using Client.FleetServiceReference;
using Client.Framework;
using Client.Views;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

namespace Client.Controller
{
    public class AddRelationController
    {
        public AddRelationWindow addRelationWindow;
        public AddRelationViewModel addRelationViewModel;

        public VehicleToEmployeeRelation AddRelation(ServiceClient socket, Vehicle vehicle)
        {
            addRelationWindow = new AddRelationWindow();

            //Filter wich Employees get shown
            
            List<Employee> emps = socket.GetAllEmployees().ToList();
            List<VehicleToEmployeeRelation> rels = socket.GetRelationFromVehicle(vehicle).ToList();
            List<Employee> selectedEmps = new List<Employee>();
            

            addRelationViewModel = new AddRelationViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                Employees = new ObservableCollection<Employee>(emps),
                Vehicle = vehicle
            };

            addRelationWindow.DataContext = addRelationViewModel;

            if (addRelationWindow.ShowDialog() == true)
            {
                var relation = addRelationViewModel.Relation;
                relation.EmployeeId = addRelationViewModel.SelectedEmployee;
                relation.VehicleId = vehicle;
                return relation;
            }
            else
            {
                return null;
            }
        }

        private void ExecuteAddCommand(object obj)
        {
            addRelationWindow.DialogResult = true;
            addRelationWindow.Close();
        }

        private void ExecuteCancelCommand(object obj)
        {
            addRelationWindow.DialogResult = false;
            addRelationWindow.Close();
        }
    }
}
