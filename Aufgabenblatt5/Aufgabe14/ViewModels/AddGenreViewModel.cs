using Aufgabe14.Infrastructure;
using Aufgabe14.Interfaces.ViewModels;
using Aufgabe14.Models;
using System.Windows.Input;

namespace Aufgabe14.ViewModels
{
    public class AddGenreViewModel : ViewModelBase, IOkCancelCommand, IAddGenreViewModel
    {
        public Genre Model { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public string Name
        {
            get => Model.Name;
            set
            {
                if (value == Model.Name) return;
                Model.Name = value;
                OnPropertyChanged();
            }
        }
    }
}
