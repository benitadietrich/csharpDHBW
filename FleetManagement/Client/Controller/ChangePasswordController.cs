using Autofac.Core;
using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Controller
{
    class ChangePasswordController
    {
        private ServiceClient socket;
        private ChangePasswordView pwView;
        private ChangePasswortViewModel pwViewModel;
        private User user;

        public ChangePasswordController(User user)
        {
            this.user = user;
        }

        public User ChangePassword(ServiceClient socket, User user)
        {
            this.socket = socket;
            pwView = new ChangePasswordView();
            pwViewModel = new ChangePasswortViewModel()
            {
                ChangePasswordCommand = new RelayCommand(ExecuteChangePasswordCommand),
            };

            pwView.DataContext = pwViewModel;

            return pwView.ShowDialog() == true? user : null;
        }

        public void ExecuteChangePasswordCommand(object obj)
        {
            var oldPassword = pwViewModel.OldPassword;
            var newPassword = pwViewModel.NewPassword;
            var newPasswordRepeat = pwViewModel.NewPasswordRepeat;
            string answer = socket.ChangePassword(user, oldPassword, newPassword, newPasswordRepeat);
            MessageBox.Show(answer);
            if (answer.Equals("Passwort wurde erfolgreich geändert"))
            {
                pwView.DialogResult = true;
                pwView.Close();
            }
        }
    }
}
