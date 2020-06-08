using Aufgabe14.Interfaces.Models;
using Aufgabe14.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IAddMovieViewModel
    {
        Movie Model { get; set; }
        List<IGenre> Genres { get; }
        ObservableCollection<IMovie> Movies { get; set; }
        string Titel { get; set; }
        Genre Genre { get; set; }
        ICommand OkCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}
