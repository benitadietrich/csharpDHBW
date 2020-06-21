using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModel;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace Client.Controller
{
    public class AddRelationController
    {
        public AddRelationView view;
        public AddRelationViewModel viewModel;
        private VehicleToEmployeeRelation relation;
        private Vehicle vehicle;
        private ServiceClient socket;

        public AddRelationController(ServiceClient socket, Vehicle vehicle)
        {
            this.vehicle = vehicle;
            this.socket = socket;
        }

        public VehicleToEmployeeRelation AddRelation()
        {
            view = new AddRelationView();

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


            viewModel = new AddRelationViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand),
                Employees = new ObservableCollection<Employee>(selectedEmps),
                Vehicle = vehicle,
            };

            viewModel.Relation.StartDate = DateTime.Now;

            view.DataContext = viewModel;


            return view.ShowDialog() == true ? relation : null;

        }

        private void ExecuteAddCommand(object obj)
        {
            relation = viewModel.Relation;
            relation.EmployeeId = viewModel.SelectedEmployee;
            relation.VehicleId = vehicle;
            if (relation != null)
            {
                if (!socket.AddRelation(relation))
                    System.Windows.Forms.MessageBox.Show("Bitte wählen Sie einen Mitarbeiter", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    System.Windows.Forms.MessageBox.Show("Relation wurde erfolgreich hinzugefügt", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    view.Close();
                }
            }

        }

        private void ExecuteCancelCommand(object obj)
        {
            view.DialogResult = false;
            view.Close();
        }
    }
}
