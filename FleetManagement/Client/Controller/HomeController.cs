using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Controller
{
    class HomeController : SubmoduleController
    {
        private User user;
        private ServiceClient socket;

        public HomeController(User user, ServiceClient socket)
        {
            this.user = user;
            this.socket = socket;
        }

        public override ViewModelBase Initialize()
        {
            return new HomeViewModel()
            {
                OpenChangePasswordCommand = new RelayCommand(ExecuteOpenChangePasswordCommand)
            };
        }

        private void ExecuteOpenChangePasswordCommand(object obj)
        {
            var passwordController = new ChangePasswordController(user);
            passwordController.ChangePassword(socket, user);
        }
    }
}
