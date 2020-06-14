using Client.FleetServiceReference;
using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Client.ViewModels
{
    class ContainerViewModel : ViewModelBase
    {
        ViewModelBase activeViewModel;

        public bool IsAdmin { get; set; }
        public ICommand OpenHomeCommand { get; set; }
        public ICommand OpenUserCommand { get; set; }

        private ICommand newCommand;
        public ICommand NewCommand
        {
            get => newCommand;
            set
            {
                newCommand = value;
                OnPropertyChanged();
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand 
        { 
            get => saveCommand; 
            set
            {
                saveCommand = value;
                OnPropertyChanged();
            }
}
        public ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get => deleteCommand;
            set
            {
                deleteCommand = value;
                OnPropertyChanged();
            }
        }


        public ViewModelBase ActiveViewModel
        {
            get { return activeViewModel; }

            set
            {
                if (activeViewModel == value)
                    return;

                activeViewModel = value;
                OnPropertyChanged("ActiveViewModel");
            }
        }
    }
}
