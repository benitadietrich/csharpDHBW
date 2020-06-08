using Aufgabe14.Interfaces.Models;
using Aufgabe14.Models;
using System.Windows.Input;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IAddGenreViewModel
    {
        Genre Model { get; set; }
        string Name { get; set; }
        ICommand OkCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}
