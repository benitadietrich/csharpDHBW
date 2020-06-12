using Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			return null;
		}
	}
}
