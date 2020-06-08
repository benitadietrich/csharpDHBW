using Aufgabe14.Interfaces.Models;

namespace Aufgabe14.Models
{
    public class Movie : IMovie
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public Genre Genre { get; set; } = new Genre();
    }
}
