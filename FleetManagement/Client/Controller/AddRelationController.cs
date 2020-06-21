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
        public AddRelationView addRelationWindow;
        public AddRelationViewModel addRelationViewModel;

        public VehicleToEmployeeRelation AddRelation(ServiceClient socket, Vehicle vehicle)
        {
            addRelationWindow = new AddRelationView();

            //Filter wich Employees get shown
            
            List<Employee> emps = socket.GetAllEmployees().ToList();
            List<VehicleToEmployeeRelation> rels = socket.GetRelationFromVehicle(vehicle).ToList();
            List<Employee> selectedEmps = new List<Employee>(emps);

            emps.ForEach(emp =>
            {
                if (rels.Find(rel => rel.EmployeeId.Id == emp.Id) != null)
                {
                    selectedEmps.Remove(emp);
                }
            });


            addRelationViewModel = new AddRelationViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                Employees = new ObservableCollection<Employee>(selectedEmps),
                Vehicle = vehicle,
            };

            addRelationViewModel.Relation.StartDate = System.DateTime.Now;

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
