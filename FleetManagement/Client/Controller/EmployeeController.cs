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
            DialogResult result = (DialogResult)MessageBox.Show("Sind Sie sicher, dass Sie den Mitarbeiter entfernen wollen?", "Relation löschen", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (socket.CannotRemoveEmployee(eViewModel.SelectedEmployee) == true)
                {
                    MessageBox.Show("Mitarbeiter kann nicht gelöscht werden, da ihm noch Fahrzeuge zugeordnet sind", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var curr = eViewModel.SelectedEmployee;
                    socket.RemoveEmployee(curr);
                }
                
            }
            else
            {
                return;
            }

            LoadModel();

        }
        public void ExecuteNewCommand(object obj)
        {
            var addEmp = new AddEmployeeController();
            addEmp.AddEmp(socket);

            LoadModel();


        }

        public void ExecuteSaveCommand(object obj)
        {
            var selectedEmp = eViewModel.SelectedEmployee;

            if (selectedEmp != null)
            {
                if (selectedEmp.EmployeeNumber == 0)
                {
                    MessageBox.Show("Bitte geben Sie eine Personalnummer an!", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    selectedEmp.BusinessUnitId = eViewModel.SelectedBusinessUnit;
                    if (!socket.EditEmployee(selectedEmp))
                        MessageBox.Show("Speicherung nicht erfolgreich, da die Daten eventuell vin einem anderen Benutzer bearbeitet wurden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            LoadModel();
        }

        public bool CanExecuteDeleteCommand(object obj)
        {
            if (eViewModel.SelectedEmployee != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
