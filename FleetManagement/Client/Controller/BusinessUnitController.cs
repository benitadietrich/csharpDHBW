using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

            bViewModel = new BusinessUnitViewModel()
            {
                BusinessUnits = new ObservableCollection<BusinessUnit>(socket.GetAllBusinessUnits()),
                SelectedUnit = null,
                EntriesUnits = socket.GetAllBusinessUnits()
            };

            container.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            container.NewCommand = new RelayCommand(ExecuteNewCommand);

            container.ActiveViewModel = bViewModel;

            return bViewModel;
        }

        public void ExecuteDeleteCommand(object obj)
        {
            var curr = bViewModel.SelectedUnit;
            bViewModel.BusinessUnits.Remove(curr);
            bViewModel.EntriesUnits.Remove(curr);
            socket.RemoveBusinessUnit(curr);

            Initialize();

        }
        public void ExecuteNewCommand(object obj)
        {
            var addUnit = new AddBusinessUnitController();
            var unit = addUnit.AddUnit(socket);
            if (unit != null)
            {
                bViewModel.BusinessUnits.Add(unit);
                bViewModel.EntriesUnits.Add(unit);
            }
            Initialize();

        }

        public void ExecuteSaveCommand(object obj)
        {
            var frontUnits = bViewModel.EntriesUnits;
            if (bViewModel.SelectedUnit != null)
                frontUnits.Add(bViewModel.SelectedUnit);
            var databaseUnits = bViewModel.BusinessUnits;
            foreach (BusinessUnit unit in databaseUnits)
            {
                foreach (BusinessUnit front in frontUnits)
                {
                    if (unit.Id == front.Id)
                    {
                        socket.EditBusinessUnit(unit);
                    }
                }
            }

            Initialize();
        }

        public bool CanExecuteDeleteCommand(object obj)
        {
            if (bViewModel.SelectedUnit != null)
            {
                if (socket.CanRemoveBusinessUnit(bViewModel.SelectedUnit) == true)
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
