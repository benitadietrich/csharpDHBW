﻿using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public ICommand OpenChangePasswordCommand { get; set; }
    }
}
