using Aufgabe14.Framework;
using Aufgabe14.Interfaces.Controller;
using Aufgabe14.Interfaces.Infrastructure;
using Aufgabe14.Interfaces.Models;
using Aufgabe14.Interfaces.ViewModels;
using Aufgabe14.ViewModels;
using Aufgabe14.Views;
using Autofac;

namespace Aufgabe14.Controller
{
    public class MainWindowController : IMainWindowController
    {
        private IMainWindowViewModel _mainWindowViewModel;

        private IRepository<IMovie> _repositoryMovie;
        private IRepository<IGenre> _repositoryGenre;
        public void Initialize(IRepository<IMovie> movieRepo, IRepository<IGenre> genreRepo, bool isInTestMode)
        {
            _repositoryMovie = movieRepo;
            _repositoryGenre = genreRepo;
            if (!isInTestMode)
            {
                var view = new MainWindow();
                _mainWindowViewModel = App.Container.Resolve<IMainWindowViewModel>();
                _mainWindowViewModel.OpenGenreCommand = new RelayCommand(ExecuteOpenGenreCommand);
                _mainWindowViewModel.OpenMovieCommand = new RelayCommand(ExecuteOpenMovieCommand);
                _mainWindowViewModel.AddCommand = new RelayCommand(ExecuteAddCommand);
                _mainWindowViewModel.DeleteCommand = new RelayCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
                view.DataContext = _mainWindowViewModel;
                ExecuteOpenMovieCommand(null);

                view.ShowDialog();
            }
        }
        
        public bool SafeDatabase(IRepository<IMovie> movieRepo, IRepository<IGenre> genreRepo, object obj)
        {
            if (movieRepo == null && genreRepo != null)
            {
                return _repositoryGenre.Save((IGenre)obj);
            }
            return _repositoryMovie.Save((IMovie)obj);
        }

        public bool DeleteDatabase(IRepository<IMovie> movieRepo, IRepository<IGenre> genreRepo, object obj)
        {
            if (movieRepo == null && genreRepo != null)
            {
                return _repositoryGenre.Delete((IGenre)obj);
            }
            return _repositoryMovie.Delete((IMovie)obj);
        }

        private void ExecuteAddCommand(object obj)
        {
            switch (_mainWindowViewModel.ActiveViewModel)
            {
                case MovieViewModel mvm:
                    var addMovieModel = App.Container.Resolve<IAddMovieViewController>();
                    var newMovie = addMovieModel.AddMovie();
                    if (newMovie is null) return;
                    mvm.MovieModels.Add(newMovie);
                    SafeDatabase(_repositoryMovie, null, newMovie);
                    UpdateMovieList();
                    break;
                case GenreViewModel gvm:
                    var addGenreModel = App.Container.Resolve<IAddGenreViewController>();
                    var newGenre = addGenreModel.AddGenre();
                    if (newGenre is null) return;
                    gvm.GenreModels.Add(newGenre);
                    SafeDatabase(null, _repositoryGenre, newGenre);
                    UpdateGenreList();
                    break;
            }
        }

        private void ExecuteDeleteCommand(object obj)
        {
            switch (_mainWindowViewModel.ActiveViewModel)
            {
                case MovieViewModel mvm:
                    var movieModel = mvm.ActualModel;
                    if (movieModel == null) return;
                    mvm.MovieModels.Remove(movieModel);
                    DeleteDatabase(_repositoryMovie, null, movieModel);
                    UpdateMovieList();
                    break;
                case GenreViewModel gvm:
                    var genreModel = gvm.ActiveModel;
                    if (genreModel == null) return;
                    gvm.GenreModels.Remove(genreModel);
                    DeleteDatabase(null, _repositoryGenre, genreModel);
                    UpdateGenreList();
                    break;
            }
        }

        private bool CanExecuteDeleteCommand(object obj)
        {
            switch (_mainWindowViewModel.ActiveViewModel)
            {
                case MovieViewModel mvm:
                    return mvm.ActualModel != null;
                case GenreViewModel gvm:
                    return gvm.ActiveModel != null;
            }
            return false;
        }

        private void ExecuteOpenGenreCommand(object obj)
        {
            var genreViewController = App.Container.Resolve<IGenreViewController>();
            _mainWindowViewModel.ActiveViewModel = genreViewController.Initialize();
            _repositoryGenre.GetAll().ForEach(genre =>
            {
                (_mainWindowViewModel.ActiveViewModel as GenreViewModel).GenreModels.Add(genre);
            });
        }
        private void ExecuteOpenMovieCommand(object obj)
        {
            var moviesViewController = App.Container.Resolve<IMovieViewController>();
            _mainWindowViewModel.ActiveViewModel = moviesViewController.Initialize();
            _repositoryMovie.GetAll().ForEach(movie =>
            {
                (_mainWindowViewModel.ActiveViewModel as MovieViewModel).MovieModels.Add(movie);
            });
        }
        private void UpdateMovieList()
        {
            (_mainWindowViewModel.ActiveViewModel as MovieViewModel).MovieModels.Clear();
            _repositoryMovie.GetAll().ForEach(m =>
            {
                (_mainWindowViewModel.ActiveViewModel as MovieViewModel).MovieModels.Add(m);
            });
        }
        private void UpdateGenreList()
        {
            (_mainWindowViewModel.ActiveViewModel as GenreViewModel).GenreModels.Clear();
            _repositoryGenre.GetAll().ForEach(m =>
            {
                (_mainWindowViewModel.ActiveViewModel as GenreViewModel).GenreModels.Add(m);
            });
        }
    }
}
