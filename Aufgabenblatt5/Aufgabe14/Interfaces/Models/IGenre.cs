namespace Aufgabe14.Interfaces.Models
{
    public interface IGenre : IDbId
    {
        string Name { get; set; }
    }
}
