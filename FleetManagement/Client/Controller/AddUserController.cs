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
    class AddUserController
    {
        private AddUserView view;
        private AddUserViewModel viewModel;
        private User user;
        private ServiceClient socket;

        public User AddUser(ServiceClient socket)
        {
            this.socket = socket;
            view = new AddUserView();
            viewModel = new AddUserViewModel()
            {
                AddUserCommand = new RelayCommand(ExecuteAddUserCommand)
            };

            view.DataContext = viewModel;

            return view.ShowDialog() == true ? user : null;
        }

        public void ExecuteAddUserCommand(object obj)
        {
            user = new User()
            {
                Firstname = viewModel.Firstname,
                Lastname = viewModel.Lastname,
                IsAdmin = viewModel.IsAdmin,
                Username = viewModel.Username
            };

            try
            {
                if(socket.AddUser(user))
                {
                    MessageBox.Show("Benutzer wurde erfolgreich hinzugefügt");
                    view.DialogResult = true;
                    view.Close();
                } else
                {
                    MessageBox.Show("Der Benutzername ist bereits vergeben");  
                }
                    

            }
            catch
            {
                MessageBox.Show("Ein Fehler ist aufgetreten überprüfen Sie ihre Eingaben");
            }

        }
    }
}
