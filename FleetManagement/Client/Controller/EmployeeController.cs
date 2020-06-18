using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using NHibernate.Mapping;
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
    class EmployeeController : SubmoduleController
    {
        public ServiceClient socket;
        private EmployeeViewModel eViewModel;
        private ContainerViewModel container;

        public EmployeeController(ServiceClient socket, ContainerViewModel container)
        {
            this.socket = socket;
            this.container = container;
        }

        public override ViewModelBase Initialize()
        {
            LoadModel();

            container.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            container.NewCommand = new RelayCommand(ExecuteNewCommand);

            return eViewModel;
        }

        void LoadModel()
        {
            int oldselid = 0;
            if (eViewModel != null)
                oldselid = eViewModel.SelectedBusinessUnit.Id;

            eViewModel = new EmployeeViewModel()
            {
                Employees = new ObservableCollection<Employee>(socket.GetAllEmployees()),
                EntriesEmployees = socket.GetAllEmployees(),
                SelectedEmployee = null,
                BusinessUnits = new ObservableCollection<BusinessUnit>(socket.GetAllBusinessUnits()),
                SelectedBusinessUnit = null,
            };
            if (oldselid != 0)
            {
                eViewModel.SelectedBusinessUnit = eViewModel.BusinessUnits.Single(x => x.Id == oldselid);
            }

            container.ActiveViewModel = eViewModel;


        }

        public void ExecuteDeleteCommand(object obj)
        {
            DialogResult result = (DialogResult)System.Windows.MessageBox.Show("Sind Sie sicher, dass Sie den Mitarbeiter entfernen wollen?", "Relation löschen", (MessageBoxButton)MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var curr = eViewModel.SelectedEmployee;
                eViewModel.Employees.Remove(curr);
                eViewModel.EntriesEmployees.Remove(curr);
                socket.RemoveEmployee(curr);
            }
            else
            {
                return;
            }

        }
        public void ExecuteNewCommand(object obj)
        {
            var addEmp = new AddEmployeeController();
            var emp = addEmp.AddEmp(socket);

            if (emp != null)
            {
                eViewModel.EntriesEmployees.Add(emp);
                eViewModel.Employees.Add(emp);
            }


        }

        public void ExecuteSaveCommand(object obj)
        {
            var selectedEmp = eViewModel.SelectedEmployee;

            if (selectedEmp != null)
            {
                if (selectedEmp.EmployeeNumber == 0)
                {
                    System.Windows.MessageBox.Show("Bitte geben Sie eine Personalnummer an!");
                }
                else
                {
                    selectedEmp.BusinessUnitId = eViewModel.SelectedBusinessUnit;
                    socket.EditEmployee(selectedEmp);
                }

            }

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

        public bool CanExecuteDeleteCommand(object obj)
        {
            if (eViewModel.SelectedEmployee != null)
            {
                if (socket.CanRemoveEmployee(eViewModel.SelectedEmployee) == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

    }
}
