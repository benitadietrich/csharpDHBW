using Autofac;
using Server;
using Server.Framework;
using Server.Interfaces;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Host
{
    class Program
    {
        public static IContainer Container
        {
            get;
            private set;
        }

        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();
            containerBuilder.RegisterType<BusinessUnitRepository>().As<IBusinessUnitRepository>();

            Container = containerBuilder.Build();
            
            Uri baseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/Server/Service/");
            using (ServiceHost host = new ServiceHost(typeof(Service), baseAddress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                var binding = new WSHttpBinding(SecurityMode.Message)
                {
                    AllowCookies = true,
                    TransactionFlow = true,
                    ReliableSession = { Enabled = false }
                };

                host.AddServiceEndpoint(typeof(IService), binding, baseAddress);
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }

        }
    }
}
