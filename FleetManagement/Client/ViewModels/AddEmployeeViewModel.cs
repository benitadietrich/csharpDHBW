using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class AddEmployeeViewModel : ViewModelBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int EmployeeNumber { get; set; }

        public string Salutation { get; set; }

        public string Title { get; set; }

        public BusinessUnit BusinessUnitId { get; set; }

        public ICommand AddEmployeeCommand { get; set; }

        public ObservableCollection<BusinessUnit> BusinessUnits { get; set; }

        private BusinessUnit selectedBusinessUnit;
        public BusinessUnit SelectedBusinessUnit
        {
            get => selectedBusinessUnit;
            set
            {
                selectedBusinessUnit = value;
                OnPropertyChanged();
            }
        }
    }
}
