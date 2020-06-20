using Client.ViewModel;
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

            if (item is EmployeeViewModel)
                return contentControl.FindResource("employeeViewTemplate") as DataTemplate;

            if (item is VehiclesViewModel)
                return contentControl.FindResource("vehicleViewTemplate") as DataTemplate;

            if (item is CostsMonthlyViewModel)
                return contentControl.FindResource("costsMonthViewTemplate") as DataTemplate;

            if (item is CostsBusinessUnitViewModel)
                return contentControl.FindResource("costsUnitViewTemplate") as DataTemplate;

            return null;
        }
    }
}
