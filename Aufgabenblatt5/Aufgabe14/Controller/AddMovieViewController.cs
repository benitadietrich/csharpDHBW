using Aufgabe14.Framework;
using Aufgabe14.Interfaces.Controller;
using Aufgabe14.Interfaces.ViewModels;
using Aufgabe14.Models;
using Aufgabe14.Views;
using Autofac;

namespace Aufgabe14.Controller
{
    public class AddMovieViewController : IAddMovieViewController
    {
        private AddMovieView _addMovieView;
        private IAddMovieViewModel _addMovieViewModel;
        public Movie AddMovie()
        {
            _addMovieView = new AddMovieView();
            _addMovieViewModel = App.Container.Resolve<IAddMovieViewModel>();
            _addMovieViewModel.Model = new Movie();
            _addMovieViewModel.OkCommand = new RelayCommand(ExecuteOkCommand);
            _addMovieViewModel.CancelCommand = new RelayCommand(ExecuteCancelCommand);

            _addMovieView.DataContext = _addMovieViewModel;
            return _addMovieView.ShowDialog() == true ? _addMovieViewModel.Model : null;
        }

        private void ExecuteOkCommand(object obj)
        {
            _addMovieView.DialogResult = true;
            _addMovieView.Close();
        }

        private void ExecuteCancelCommand(object obj)
        {
            _addMovieView.DialogResult = false;
            _addMovieView.Close();
        }
    }
}
