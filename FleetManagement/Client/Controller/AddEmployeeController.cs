using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Controller
{
    class AddEmployeeController
    {
        private AddEmployeeView view;
        private AddEmployeeViewModel viewModel;
        private ServiceClient socket;
        private Employee emp;

        public Employee AddEmp(ServiceClient socket)
        {
            this.socket = socket;
            view = new AddEmployeeView();
            viewModel = new AddEmployeeViewModel()
            {
                AddEmployeeCommand = new RelayCommand(ExecuteAddEmployeeCommand),
                BusinessUnits = new ObservableCollection<BusinessUnit>(socket.GetAllBusinessUnits())
            };

            view.DataContext = viewModel;

            return view.ShowDialog() == true ? emp : null;
        }

        public void ExecuteAddEmployeeCommand(object obj)
        {

            emp = new Employee()
            {
                EmployeeNumber = viewModel.EmployeeNumber,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Salutation = viewModel.Salutation,
                Title = viewModel.Title,
                BusinessUnitId = viewModel.SelectedBusinessUnit
            };
            if (emp.EmployeeNumber == 0|| emp.FirstName == null || emp.FirstName == "" || emp.LastName == "" || emp.LastName == null )
            {
                MessageBox.Show("Ungültige Werte", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (socket.AddEmployee(emp))
                    {
                        MessageBox.Show("Mitarbeiter wurde erfolgreich hinzugefügt","Mitarbeiter hinzugefügt", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        view.DialogResult = true;
                        view.Close();
                    }
                    else
                    {
                        MessageBox.Show("Diese Personalnummer ist bereits vergeben", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                catch
                {
                    MessageBox.Show("Ein Fehler ist aufgetreten überprüfen Sie ihre Eingaben", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
