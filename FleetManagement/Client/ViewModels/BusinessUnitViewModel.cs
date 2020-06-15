using Client.FleetServiceReference;
using Client.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    class BusinessUnitViewModel : ViewModelBase
    {
        public ObservableCollection<BusinessUnit> BusinessUnits { get; set; }

        private BusinessUnit selectedUnit;
        public BusinessUnit SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                OnPropertyChanged();
            }
        }

        public List<BusinessUnit> EntriesUnits { get; set; }
    }
}
