using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller
{
    public class LoginController
    {
        private LoginViewModel _loginViewModel;
        private LoginView _loginWindow;
        private ServiceClient _socket;
        private User _user;

        public User LoginUser(ServiceClient socket)
        {
            _socket = socket;
            _loginWindow = new LoginView();
            _loginViewModel = new LoginViewModel()
            {
                LoginCommand = new RelayCommand(ExecuteLoginCommand)
            };

            _loginWindow.DataContext = _loginViewModel;

            return _loginWindow.ShowDialog() == true ? _user : null;
        }

        private void ExecuteLoginCommand(object obj)
        {
            var username = _loginViewModel.Username;
            var password = _loginViewModel.Password;
            _user = _socket.Login(username, password);
            if (_user != null)
            {
                _loginWindow.DialogResult = true;
                _loginWindow.Close();
            }
            else
            {
                MessageBox.Show("Login war nicht erfolgreich");
            }
        }

    }
}
