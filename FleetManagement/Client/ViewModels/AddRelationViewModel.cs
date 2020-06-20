using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class AddRelationViewModel: ViewModelBase
    {
        public ICommand AddCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private VehicleToEmployeeRelation relation = new VehicleToEmployeeRelation();
        public VehicleToEmployeeRelation Relation
        {
            get
            {
                return relation;
            }
            set
            {
                relation = value;
                OnPropertyChanged();
            }
        }

        public Vehicle Vehicle { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get
            {
                return selectedEmployee;
            }
            set
            {
                selectedEmployee = value;

                if (selectedEmployee != null)
                    Relation.EmployeeId = value;

                OnPropertyChanged();
            }
        }
    }
}
