using Aufgabe11.Framework;
using Aufgabe11.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Aufgabe11.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Employee _selectedModel;
        public ObservableCollection<Employee> Models { get; set; } = new ObservableCollection<Employee>();
        public Employee SelectedModel {
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
