using Client.Framework;
using Client.Models;
using System.Collections.ObjectModel;

namespace Client.ViewModels
{
    class CostsMonthlyViewModel : ViewModelBase
    {
        public ObservableCollection<CostsMonthlyModel> Costs { get; set; }
    }
}
