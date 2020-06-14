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
            };

            var loginWindowController = new LoginController();
            user = loginWindowController.LoginUser(socket);

            containerViewModel.ActiveViewModel = new HomeController(user, socket).Initialize();
            containerView.DataContext = containerViewModel;

            if (user != null)
            {
                containerViewModel.IsAdmin = user.IsAdmin;
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

        private void ExecuteOpenUserCommand(object obj)
        {
            var userController = new UserController(user, socket);
            containerViewModel.ActiveViewModel = userController.Initialize();
            containerViewModel.DeleteCommand = new RelayCommand(userController.ExecuteDeleteCommand, userController.CanExecuteDeleteCommand);
            containerViewModel.SaveCommand = new RelayCommand(userController.ExecuteSaveCommand);
            containerViewModel.NewCommand = new RelayCommand(userController.ExecuteNewCommand);
        }

    }
}
