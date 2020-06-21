using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModels
{
    class EmployeeViewModel : ViewModelBase
    {
        public ObservableCollection<Employee> Employees { get; set; }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;

                if (selectedEmployee != null)
                    SelectedBusinessUnit = BusinessUnits.Single(x=> x.Id== selectedEmployee.BusinessUnitId.Id);
                OnPropertyChanged();
                
            }
        }

        public  int index { get; set; }

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

