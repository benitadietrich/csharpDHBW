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
                OpenUserCommand = new RelayCommand(ExecuteOpenUserCommand),
                OpenUnitCommand = new RelayCommand(ExecuteOpenUnitCommand),
                LogoutCommand = new RelayCommand(ExecuteLogout)
            };

            Login();

        }

        private void Login()
        {
            var loginWindowController = new LoginController();
            user = loginWindowController.LoginUser(socket);

            containerViewModel.ActiveViewModel = new HomeController(user, socket).Initialize();
            containerView.DataContext = containerViewModel;

            if (user != null)
            {
                containerViewModel.IsAdmin = user.IsAdmin;
                containerView.ShowDialog();
            }
            else
            {
                containerView.Close();
            }
        }

        private void ExecuteOpenHomeCommand(object obj)
        {
            var homeController = new HomeController(user, socket);
            containerViewModel.ActiveViewModel = homeController.Initialize();
        }

        private void ExecuteOpenUserCommand(object obj)
        {
            var userController = new UserController(user, socket, containerViewModel);
            containerViewModel.ActiveViewModel = userController.Initialize();
        }

        private void ExecuteOpenUnitCommand(object obj)
        {
            var unitController = new BusinessUnitController(socket, containerViewModel);
            containerViewModel.ActiveViewModel = unitController.Initialize();
            
        }

        private void ExecuteLogout(object obj)
        {
            containerView.Hide();
            Login();
        }

    }
}
