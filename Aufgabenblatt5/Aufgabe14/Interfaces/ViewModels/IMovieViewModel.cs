using System.Collections.ObjectModel;
using Aufgabe14.Interfaces.Models;
using Aufgabe14.Models;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IMovieViewModel
    {
        ObservableCollection<IMovie> MovieModels { get; set; }
        Movie ActualModel { get; set; }
    }
}
