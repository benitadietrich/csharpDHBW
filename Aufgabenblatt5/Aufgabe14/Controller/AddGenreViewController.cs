using Aufgabe14.Framework;
using Aufgabe14.Interfaces.Controller;
using Aufgabe14.Interfaces.ViewModels;
using Aufgabe14.Models;
using Aufgabe14.Views;
using Autofac;

namespace Aufgabe14.Controller
{
    public class AddGenreViewController : IAddGenreViewController
    {
        private AddGenreView _addGenreView;
        private IAddGenreViewModel _addGenreViewModel;
        public Genre AddGenre()
        {
            _addGenreView = new AddGenreView();
            _addGenreViewModel = App.Container.Resolve<IAddGenreViewModel>();
            _addGenreViewModel.Model = new Genre();
            _addGenreViewModel.OkCommand = new RelayCommand(ExecuteOkCommand);
            _addGenreViewModel.CancelCommand = new RelayCommand(ExecuteCancelCommand);

            _addGenreView.DataContext = _addGenreViewModel;
            return _addGenreView.ShowDialog() == true ? _addGenreViewModel.Model : null;
        }

        private void ExecuteOkCommand(object obj)
        {
            _addGenreView.DialogResult = true;
            _addGenreView.Close();
        }

        private void ExecuteCancelCommand(object obj)
        {
            _addGenreView.DialogResult = false;
            _addGenreView.Close();
        }
    }
}
