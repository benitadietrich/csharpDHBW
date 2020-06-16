using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            eViewModel = new EmployeeViewModel()
            {
                Employees = new ObservableCollection<Employee>(socket.GetAllEmployees()),
                EntriesEmployees = socket.GetAllEmployees(),
                SelectedEmployee = null,
                BusinessUnits = new ObservableCollection<BusinessUnit>( socket.GetAllBusinessUnits()),
                SelectedBusinessUnit = null,
            };

            container.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            container.NewCommand = new RelayCommand(ExecuteNewCommand);

            container.ActiveViewModel = eViewModel;

            return eViewModel;
        }

        public void ExecuteDeleteCommand(object obj)
        {
            var curr = eViewModel.SelectedEmployee;
            eViewModel.Employees.Remove(curr);
            eViewModel.EntriesEmployees.Remove(curr);
            socket.RemoveEmployee(curr);

            Initialize();

        }
        public void ExecuteNewCommand(object obj)
        {
            var addEmp = new AddEmployeeController();
            var emp = addEmp.AddEmp(socket);

            if(emp != null)
            {
                eViewModel.EntriesEmployees.Add(emp);
                eViewModel.Employees.Add(emp);
            }

            Initialize();

        }

        public void ExecuteSaveCommand(object obj)
        {
            var selectedEmp = eViewModel.SelectedEmployee;

            var frontEmps = eViewModel.EntriesEmployees;
            if (selectedEmp != null)
            {
                selectedEmp.BusinessUnitId = eViewModel.SelectedBusinessUnit;
                frontEmps.Add(selectedEmp);
            }
            var databaseEmps = eViewModel.Employees;
            foreach (Employee emp in databaseEmps)
            {
                foreach (Employee front in frontEmps)
                {
                    if (emp.Id == front.Id)
                    {
                        socket.EditEmployee(emp);
                    }
                }
            }

            Initialize();
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
