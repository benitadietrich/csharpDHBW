using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using Client.Views;
using System.Windows;

namespace Client.Controller
{
    class ContainerController
    {
        private ContainerViewModel containerViewModel;
        private ContainerView containerView;
        private ServiceClient socket;
        private User user;

        public void Initialize()
        {
            socket = new ServiceClient();
            containerView = new ContainerView();
            containerViewModel = new ContainerViewModel()
            {
                OpenHomeCommand = new RelayCommand(ExecuteOpenHomeCommand),
            };

            containerViewModel.ActiveViewModel = new HomeController(user, socket).Initialize();

            containerView.DataContext = containerViewModel;
            var loginWindowController = new LoginController();
            user = loginWindowController.LoginUser(socket);
            if (user != null)
            {
                containerView.ShowDialog();
            } else
            {
                containerView.Close();
            }

        }

        private void ExecuteOpenHomeCommand(object obj)
        {
            var homeController = new HomeController(user, socket);
            containerViewModel.ActiveViewModel = homeController.Initialize();
        }
    }
}
