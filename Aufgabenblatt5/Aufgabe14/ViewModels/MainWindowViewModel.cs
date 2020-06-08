using System.Windows.Input;
using Aufgabe14.Infrastructure;
using Aufgabe14.Interfaces.ViewModels;

namespace Aufgabe14.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private ViewModelBase _mActiveViewModel;

        public ICommand OpenGenreCommand { get; set; }
        public ICommand OpenMovieCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ViewModelBase ActiveViewModel
        {
            get => _mActiveViewModel;

            set
            {
                if (_mActiveViewModel == value)
                    return;

                _mActiveViewModel = value;
                OnPropertyChanged();
            }
        }
	}
}
