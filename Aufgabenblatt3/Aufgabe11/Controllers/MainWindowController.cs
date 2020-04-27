using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe11.ViewModels;
using Aufgabe11.Models;
using Aufgabe11.Controllers;
using System.Windows.Navigation;
using Beispiel_1.Framework;
using Aufgabe11.Views;

namespace Aufgabe11.Controllers
{
    class MainWindowController
    {
        private MainWindowViewModel mViewModel;

        void ExecuteAddCommand(object obj)
        {
            var winAddController = new WindowAddController();
            var emp = winAddController.AddEmployee();
            if(emp!= null)
            {
                mViewModel.Models.Add(emp);
            }
        }

        void ExecuteDeleteCommand(object obj)
        {
            if (mViewModel.SelectedModel != null)
            {
                mViewModel.Models.Remove(mViewModel.SelectedModel);
            }
        }

        bool CanExecuteDeleteCommand(object obj)
        {
            return mViewModel.SelectedModel != null ? true : false;
        }

        public void Initialize()
        {
            mViewModel = new MainWindowViewModel()
            {
                AddCommand = new RelayCommand(ExecuteAddCommand),
                DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand)
            };
            var view = new MainWindow()
            {
                DataContext = mViewModel
            };

            view.ShowDialog();
        }
    }
}
