using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Aufgabe12_Client.CustomerServiceProxy;
using Aufgabe12_Client.ViewModels;
using Aufgabe12_Client.Views;
using Aufgabe12_Client.Framework;

namespace Aufgabe12_Client.Controllers
{
    class AddWindowController
    {
        private AddWindow _mView;

        void ExecuteOkCommand(object obj)
        {
            _mView.DialogResult = true;
            _mView.Close();
        }

        void ExecuteCancelCommand(object o)
        {
            _mView.DialogResult = false;
            _mView.Close();
        }

        public Customer AddCustomer()
        {
            _mView = new AddWindow();
            var temp = new AddWindowViewModel()
            {
                Model = new Customer(),
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            _mView.DataContext = temp;
            _mView.Owner = Application.Current.MainWindow;
            return _mView.ShowDialog() == true ? temp.Model : null;
        }
    }
}
