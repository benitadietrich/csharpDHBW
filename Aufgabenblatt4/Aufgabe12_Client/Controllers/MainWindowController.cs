using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe12_Client.CustomerServiceProxy;
using Aufgabe12_Client.Framework;
using Aufgabe12_Client.ViewModels;

namespace Aufgabe12_Client.Controllers
{
    class MainWindowController
    {
        private MainWindowViewModel _mViewModel;
        private CustomerServiceClient _service;

        void ExecuteNewCommand(object obj)
        {
            var winAddController = new AddWindowController();
            var cus = winAddController.AddCustomer();
            if (cus != null)
            {
                _service.AddCustomer(cus);
            }
            ExecuteLoadCommand(obj);

        }

        void ExecuteDeleteCommand(object obj)
        {
            var cus = _mViewModel.SelectedModel;
            _service.RemoveCustomer(cus);
            ExecuteLoadCommand(obj);
        }

        void ExecuteSearchCommand(object obj)
        {
            ExecuteEmptyCommand(obj);
            if (_mViewModel.SearchQuery.Equals(""))
            {
               ExecuteLoadCommand(obj);
            }
            else
            {
                _service.GetCustomers(_mViewModel.SearchQuery).ForEach(customer => _mViewModel.Models.Add(customer));
            }
        }

        void ExecuteEmptyCommand(object obj)
        {
            _mViewModel.Models.Clear();
        }


        bool CanExecuteDeleteCommand(object obj)
        {
            return _mViewModel.SelectedModel != null ? true : false;
        }

        void ExecuteLoadCommand(object obj)
        {
            ExecuteEmptyCommand(obj);
            _service.GetAllCustomers().ForEach(customer => _mViewModel.Models.Add(customer));
        }

        public void Initialize()
        {
            _service = new CustomerServiceClient();
            _mViewModel = new MainWindowViewModel()
            {
                NewCommand = new RelayCommand(ExecuteNewCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand),
                LoadCommand = new RelayCommand(ExecuteLoadCommand),
                EmptyCommand = new RelayCommand(ExecuteEmptyCommand),
                SearchCommand = new RelayCommand(ExecuteSearchCommand),
                SearchQuery = "Bitte Suchtext eingeben..."
            };


            var view = new MainWindow
            {
                DataContext = _mViewModel
            };

            view.ShowDialog();
        }
    }
}
