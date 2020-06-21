using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Controller
{
    class BusinessUnitController : SubmoduleController
    {
        public ServiceClient socket;
        private BusinessUnitViewModel bViewModel;
        private ContainerViewModel container;

        public BusinessUnitController(ServiceClient socket, ContainerViewModel container)
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

            return bViewModel;
        }

        public void LoadModel()
        {
            bViewModel = new BusinessUnitViewModel()
            {
                BusinessUnits = new ObservableCollection<BusinessUnit>(socket.GetAllBusinessUnits()),
                SelectedUnit = null
            };
            container.ActiveViewModel = bViewModel;
        }

        public void ExecuteDeleteCommand(object obj)
        {
            if (socket.CannotRemoveBusinessUnit(bViewModel.SelectedUnit) == true)
            {
                MessageBox.Show("Diesem Objekt sind noch Daten zugeordnet.", "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                var curr = bViewModel.SelectedUnit;
                socket.RemoveBusinessUnit(curr);

            }

            LoadModel();

        }
        public void ExecuteNewCommand(object obj)
        {
            var addUnit = new AddBusinessUnitController();
            var unit = addUnit.AddUnit(socket);
            if (unit != null)
            {
                bViewModel.BusinessUnits.Add(unit);
            }

            LoadModel();

        }

        public void ExecuteSaveCommand(object obj)
        {
            var selectedUnit = bViewModel.SelectedUnit;

            if (selectedUnit != null)
            {
                if (!socket.EditBusinessUnit(selectedUnit))
                    MessageBox.Show("Die Daten konnten nicht gespeichert werden, da sie eventuell von einem anderen Benutzer bearbeitet wurden.", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            LoadModel();
        }

        public bool CanExecuteDeleteCommand(object obj)
        {
            if (bViewModel.SelectedUnit != null)
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
