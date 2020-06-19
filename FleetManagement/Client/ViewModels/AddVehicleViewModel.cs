using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class AddVehicleViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string LicensePlate { get; set; }

        public string Model { get; set; }

        public float Insurance { get; set; }

        public DateTime LeasingFrom { get; set; }

        public DateTime LeasingTo { get; set; }

        public float LeasingRate { get; set; }

        public ICommand AddCommand { get; set; }
    }
}
