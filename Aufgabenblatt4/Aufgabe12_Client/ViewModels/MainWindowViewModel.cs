using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Aufgabe12_Client.CustomerServiceProxy;
using Aufgabe12_Client.Framework;

namespace Aufgabe12_Client.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private Customer _selectedModel;
        private string _searchQuery;

        public ObservableCollection<Customer> Models { get; set; } = new ObservableCollection<Customer>();

        public Customer SelectedModel
        {
            get => _selectedModel;
            set
            {
                _selectedModel = value;
                OnPropertyChanged();
            }
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand NewCommand { get; set; }
        public ICommand EmptyCommand { get; set; }
        public ICommand SearchCommand { get; set; }
    }
}
