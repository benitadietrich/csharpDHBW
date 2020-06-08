using Aufgabe14.Interfaces.Infrastructure;
using Aufgabe14.Interfaces.Models;

namespace Aufgabe14.Interfaces.Controller
{
    public interface IMainWindowController
    {
        void Initialize(IRepository<IMovie> movieRepo, IRepository<IGenre> genreMovie, bool isInTestMode);
    }
}
