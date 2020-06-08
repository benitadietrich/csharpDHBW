using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe13.Framework;
using Aufgabe13.Models;
using Aufgabe13.ViewModels;
using Aufgabe13.Views;

namespace Aufgabe13.Controllers
{
    class WindowAddController
    {
        private WindowAdd mView;

        void ExecuteOkCommand(object obj)
        {
            mView.DialogResult = true;
            mView.Close();
        }

        void ExecuteCancelCommand(object o)
        {
            mView.DialogResult = false;
            mView.Close();
        }

        public Customer AddCustomer()
        {
            mView = new WindowAdd();
            var temp = new WindowAddViewModel()
            {
                Model = new Customer(),
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            mView.DataContext = temp;
            return mView.ShowDialog() == true ? temp.Model : null;
        }
    }
}
