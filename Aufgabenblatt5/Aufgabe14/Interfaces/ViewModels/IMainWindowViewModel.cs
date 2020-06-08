using Aufgabe14.Infrastructure;
using System.Windows.Input;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IMainWindowViewModel
    {
        ICommand AddCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ICommand OpenGenreCommand { get; set; }
        ICommand OpenMovieCommand { get; set; }
        ViewModelBase ActiveViewModel { get; set; }
    }
}
