using Aufgabe14.Infrastructure;
using Aufgabe14.Interfaces.Controller;
using Aufgabe14.ViewModels;

namespace Aufgabe14.Controller
{
    public class MoviesViewController : SubmoduleController, IMovieViewController
    {
        public override ViewModelBase Initialize()
        {
            return new MovieViewModel();
        }
    }
}
