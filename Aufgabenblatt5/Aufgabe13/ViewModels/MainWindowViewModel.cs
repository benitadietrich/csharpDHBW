using Aufgabe13.Framework;
using Aufgabe13.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabe13.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Customer _selectedModel;
        public ObservableCollection<Customer> Models { get; set; } = new ObservableCollection<Customer>();
        public Customer SelectedModel {
            get => _selectedModel;
            set
            {          
                _selectedModel = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

    }
}
