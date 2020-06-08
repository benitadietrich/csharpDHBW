using System.Windows;
using System.Windows.Controls;
using Aufgabe14.ViewModels;

namespace Aufgabe14.Views
{
    public class SubmoduleSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contentControl = (container as FrameworkElement);

            switch (item)
            {
                case MovieViewModel _:
                    return contentControl?.FindResource("movieViewTemplate") as DataTemplate;
                case GenreViewModel _:
                    return contentControl?.FindResource("genreViewTemplate") as DataTemplate;
                default:
                    return null;
            }
        }
    }
}
