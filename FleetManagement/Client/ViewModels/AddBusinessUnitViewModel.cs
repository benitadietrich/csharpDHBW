using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class AddBusinessUnitViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICommand AddBusinessUnitCommand {get; set;}
    }
}
