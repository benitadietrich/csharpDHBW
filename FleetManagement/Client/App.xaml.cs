using Autofac;
using Client.Controller;
using Client.FleetServiceReference;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var containerBuilder = new ContainerBuilder();

            //Add main controller
            containerBuilder.RegisterType<ContainerController>();

            //Add controller
            containerBuilder.RegisterType<AddBusinessUnitController>();
            containerBuilder.RegisterType<AddEmployeeController>();
            containerBuilder.RegisterType<AddUserController>();
            containerBuilder.RegisterType<BusinessUnitController>();
            containerBuilder.RegisterType<ChangePasswordController>();

 
            containerBuilder.RegisterType<EmployeeController>();
            containerBuilder.RegisterType<HomeController>();
            containerBuilder.RegisterType<LoginController>();
            containerBuilder.RegisterType<UserController>();


            //Add view
            containerBuilder.RegisterType<AddBusinessUnitView>();
            containerBuilder.RegisterType<AddEmployeeView>();
            containerBuilder.RegisterType<AddUserView>();

            containerBuilder.RegisterType<CostsMonthlyView>();
            containerBuilder.RegisterType<EmployeeView>();
            containerBuilder.RegisterType<ContainerView>();
            containerBuilder.RegisterType<HomeView>();
            containerBuilder.RegisterType<LoginView>();
            containerBuilder.RegisterType<UserView>();
            containerBuilder.RegisterType<VehiclesView>();

            //Server
            containerBuilder.RegisterType<ServiceClient>();

            Container = containerBuilder.Build();

            var fleetManagementController = Container.Resolve<ContainerController>();

            fleetManagementController.Initialize();
        }
    }
}
