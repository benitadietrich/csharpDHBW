using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Aufgabe12_Client.CustomerServiceProxy;
using Aufgabe12_Client.Framework;

namespace Aufgabe12_Client.ViewModels
{
    class AddWindowViewModel : ViewModelBase
    {
        public Customer Model { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (Model.FirstName == value) return;
                Model.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => Model.LastName;
            set
            {
                if (Model.LastName == value) return;
                Model.LastName = value;
                OnPropertyChanged();
            }
        }
    }
}
