using System.Windows.Input;

namespace Aufgabe14.Interfaces.ViewModels
{
    public interface IOkCancelCommand
    {
        ICommand OkCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}
