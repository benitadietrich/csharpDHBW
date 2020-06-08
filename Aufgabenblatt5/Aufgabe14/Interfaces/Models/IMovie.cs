using Aufgabe14.Models;

namespace Aufgabe14.Interfaces.Models
{
    public interface IMovie : IDbId
    {
        string Titel { get; set; }
        Genre Genre { get; set; }
    }
}
