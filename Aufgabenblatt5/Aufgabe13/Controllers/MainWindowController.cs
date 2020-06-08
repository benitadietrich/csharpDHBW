using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe13.Framework;
using Aufgabe13.Models;
using Aufgabe13.ViewModels;
using Aufgabe13.Views;

namespace Aufgabe13.Controllers
{
    class MainWindowController
    {
        private MainWindowViewModel mViewModel;
        private Repository<Customer> mCustomerRepository;

        void ExecuteAddCommand(object obj)
        {
            var winAddController = new WindowAddController();
            var cus = winAddController.AddCustomer();
            if (cus != null)
            {
                mViewModel.Models.Add(cus);
                mCustomerRepository.Save(cus);
            }
        }

        void ExecuteDeleteCommand(object obj)
        {
            if (mViewModel.SelectedModel != null)
            {
                mCustomerRepository.Delete(mViewModel.SelectedModel);
                mViewModel.Models.Remove(mViewModel.SelectedModel);
            }
        }

        bool CanExecuteDeleteCommand(object obj)
        {
            return mViewModel.SelectedModel != null ? true : false;
        }

        public void Initialize()
        {
            mCustomerRepository = new Repository<Customer>(@"Database\CustomerSample.db");
            mViewModel = new MainWindowViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand),
                Models = new ObservableCollection<Customer>(mCustomerRepository.GetAll())
            };
            var view = new MainWindow()
            {
                DataContext = mViewModel
            };

            view.ShowDialog();
        }
    }
}
