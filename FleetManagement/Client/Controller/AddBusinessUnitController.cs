using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller
{
    class AddBusinessUnitController
    {
        private AddBusinessUnitView view;
        private AddBusinessUnitViewModel viewModel;
        private BusinessUnit businessUnit;
        private ServiceClient socket;

        public BusinessUnit AddUnit(ServiceClient socket)
        {
            this.socket = socket;
            view = new AddBusinessUnitView();
            viewModel = new AddBusinessUnitViewModel()
            {
                AddBusinessUnitCommand = new RelayCommand(ExecuteAddUnitCommand)
            };

            view.DataContext = viewModel;

            return view.ShowDialog() == true ? businessUnit : null;
        }

        public void ExecuteAddUnitCommand(object obj)
        {
            businessUnit = new BusinessUnit()
            {
                Name = viewModel.Name,
                Description = viewModel.Description
            };

            try
            {
                if (socket.AddBusinessUnit(businessUnit))
                {
                    MessageBox.Show("Geschäftsbereich wurde erfolgreich hinzugefügt");
                    view.DialogResult = true;
                    view.Close();
                }
                else
                {
                    MessageBox.Show("Der Geschäftsbereich wurde bereits erstellt");
                }


            }
            catch
            {
                MessageBox.Show("Ein Fehler ist aufgetreten überprüfen Sie ihre Eingaben");
            }

        }
    }
}
