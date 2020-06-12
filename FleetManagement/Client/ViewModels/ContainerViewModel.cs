using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class ContainerViewModel : ViewModelBase
    {
        ViewModelBase activeViewModel;

		public ICommand OpenHomeCommand { get; set; }


		public ViewModelBase ActiveViewModel
		{
			get { return activeViewModel; }

			set
			{
				if (activeViewModel == value)
					return;

				activeViewModel = value;
				OnPropertyChanged("ActiveViewModel");
			}
		}
	}
}
