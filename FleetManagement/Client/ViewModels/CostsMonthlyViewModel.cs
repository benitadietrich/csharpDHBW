using Client.Framework;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    class CostsMonthlyViewModel : ViewModelBase
    {

        public Dictionary<DateTime, CostsMonthlyModel> Costs
        {
            get;
            set;
        }
    }
}
