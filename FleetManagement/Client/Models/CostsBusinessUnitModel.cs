using Client.FleetServiceReference;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models
{
    class CostsBusinessUnitModel
    {
        public string Month { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
        public decimal Costs { get; set; }
        public string CostsDisplay { get; set; }

        public Dictionary<DateTime, Dictionary<BusinessUnit, decimal>> BusCosts { get; set; }
    }
}
