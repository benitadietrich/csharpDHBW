using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System.Windows.Forms;

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

            if (businessUnit.Name == "" || businessUnit.Name == null)
                MessageBox.Show("Ungültige Eingaben", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    if (socket.AddBusinessUnit(businessUnit))
                    {
                        MessageBox.Show("Geschäftsbereich wurde erfolgreich hinzugefügt", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        view.DialogResult = true;
                        view.Close();
                    }
                    else
                    {
                        MessageBox.Show("Der Geschäftsbereich wurde bereits erstellt", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
