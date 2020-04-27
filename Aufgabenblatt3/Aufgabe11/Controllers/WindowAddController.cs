using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aufgabe11.Views;
using Aufgabe11.ViewModels;
using Aufgabe11.Models;
using System.Runtime.InteropServices;
using Beispiel_1.Framework;

namespace Aufgabe11.Controllers
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

        public Employee AddEmployee()
        {
            mView = new WindowAdd();
            var temp = new WindowAddViewModel()
            {
                Model = new Employee(),
                OkCommand = new RelayCommand(ExecuteOkCommand),
                CancelCommand = new RelayCommand(ExecuteCancelCommand)
            };
            mView.DataContext = temp;
            return mView.ShowDialog() == true ? temp.Model : null;
        }
    }
}
