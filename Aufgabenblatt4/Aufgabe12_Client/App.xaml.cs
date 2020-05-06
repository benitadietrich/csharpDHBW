using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Aufgabe12_Client.Controllers;

namespace Aufgabe12_Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs args)
        {
            var controller = new MainWindowController();
            controller.Initialize();
        }
    }
}
