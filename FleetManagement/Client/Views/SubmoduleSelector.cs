using Client.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Client.Views
{
    public class SubmoduleSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contentControl = (container as FrameworkElement);

            if (item is HomeViewModel)
                return contentControl.FindResource("homeViewTemplate") as DataTemplate;

            if (item is UserViewModel)
                return contentControl.FindResource("userViewTemplate") as DataTemplate;

            if (item is BusinessUnitViewModel)
                return contentControl.FindResource("businessUnitViewTemplate") as DataTemplate;

            return null;
        }
    }
}
