using Client.Framework;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    class CostsMonthlyViewModel : ViewModelBase
    {

        public Dictionary<string, CostsMonthlyModel> Costs 
        {
            get;
            set;
        }
    }
}
