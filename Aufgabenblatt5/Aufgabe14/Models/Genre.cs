using Aufgabe14.Interfaces.Models;

namespace Aufgabe14.Models
{
    public class Genre : IGenre
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
