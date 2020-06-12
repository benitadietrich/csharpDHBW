using Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Client.ViewModels
{
    class ChangePasswortViewModel : ViewModelBase
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordRepeat { get; set; }

        public ICommand ChangePasswordCommand { get; set; }

    }
}
