using Client.FleetServiceReference;
using Client.Framework;
using Client.ViewModels;
using System.Collections.ObjectModel;

namespace Client.Controller
{
    class UserController : SubmoduleController
    {
        public User user;
        public ServiceClient socket;
        private UserViewModel uViewModel;
        private ContainerViewModel container;

        public UserController(User user, ServiceClient socket, ContainerViewModel container)
        {
            this.user = user;
            this.socket = socket;
            this.container = container;
        }

        public override ViewModelBase Initialize()
        {

            uViewModel = new UserViewModel()
            {
                Users = new ObservableCollection<User>(socket.GetAllUsers()),
                SelectedUser = null,
                EntriesUser = socket.GetAllUsers()
            };

            container.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            container.SaveCommand = new RelayCommand(ExecuteSaveCommand);
            container.NewCommand = new RelayCommand(ExecuteNewCommand);

            container.ActiveViewModel = uViewModel;

            return uViewModel;
        }

        public void ExecuteDeleteCommand(object obj)
        {
            var curr = uViewModel.SelectedUser;
            if (user.Username != curr.Username)
            {
                uViewModel.Users.Remove(curr);
                uViewModel.EntriesUser.Remove(curr);
                socket.DeleteUser(curr);
            }
            else if (curr != null)
            {
                System.Windows.Forms.MessageBox.Show("Ein Fehler ist aufgetreten, bitte versuchen Sie es noch einmal");
            }

            Initialize();

        }
        public void ExecuteNewCommand(object obj)
        {
            var addUser = new AddUserController();
            var user = addUser.AddUser(socket);
            if (user != null)
            {
                uViewModel.Users.Add(user);
                uViewModel.EntriesUser.Add(user);
            }
            Initialize();
        }

        public void ExecuteSaveCommand(object obj)
        {
            var frontUsers = uViewModel.EntriesUser;
            if (uViewModel.SelectedUser != null) frontUsers.Add(uViewModel.SelectedUser);
            var databaseUsers = uViewModel.Users;
            foreach (User user in databaseUsers)
            {
                foreach (User front in frontUsers)
                {
                    if (user.Id == front.Id)
                    {
                        socket.EditUser(user);
                    }

                }
            }


            Initialize();
        }

        public bool CanExecuteDeleteCommand(object obj)
        {
            if(uViewModel.SelectedUser != null)
            {
                if(uViewModel.SelectedUser.Username == user.Username)
                {
                    return false;
                } else
                {
                    return true;
                }
            } else
            {
                return false;
            }

        }
    }
}
