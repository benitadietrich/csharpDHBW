using Client.FleetServiceReference;
using Client.Framework;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels
{
    class CostsBusinessUnitViewModel : ViewModelBase
    {
        public Dictionary<DateTime, CostsBusinessUnitModel> Costs { get; set; }
    }
}
