using Aufgabe14.Interfaces.Models;
using Aufgabe14.Models;
using System.Collections.ObjectModel;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IGenreViewModel
    {
        ObservableCollection<IGenre> GenreModels { get; set; }
        Genre ActiveModel { get; set; }
    }
}
