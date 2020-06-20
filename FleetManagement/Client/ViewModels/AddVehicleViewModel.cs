using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class AddVehicleViewModel: ViewModelBase
    {
        public ICommand AddCommand { get; set; }

        private Vehicle vehicle = new Vehicle() { LeasingFrom = DateTime.Now , LeasingTo = DateTime.Now };
        public Vehicle Vehicle
        {
            get
            {
                return vehicle;
            }
            set
            {
                vehicle = value;
                OnPropertyChanged();
            }
        }
    }
}
