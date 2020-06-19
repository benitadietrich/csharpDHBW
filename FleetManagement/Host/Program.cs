using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        public static void Main(string[] _args)
        {
            Console.WriteLine("Starting FleetManagement Service...");

            // Create base address
            Uri serviceAddress = new Uri("http://localhost:8733/Design_Time_Addresses/Server/Service/");
            // Create ServiceHost
            using (ServiceHost host = new ServiceHost(typeof(Service), serviceAddress))
            {
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = {
                        PolicyVersion = PolicyVersion.Policy15
                    }
                };
                ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
                sdb.IncludeExceptionDetailInFaults = true;

                host.Description.Behaviors.Add(smb);
                host.Description.Behaviors.Remove<ServiceDebugBehavior>();
                host.Description.Behaviors.Add(sdb);

                WSHttpBinding wsBinding = new WSHttpBinding(SecurityMode.Message)
                {
                    AllowCookies = true,
                    ReliableSession = {
                        Enabled = true
                    },
                    TransactionFlow = false
                };

                host.AddServiceEndpoint(typeof(IService), wsBinding, serviceAddress);

                host.Open();

                Console.WriteLine($"Burgerama Service ready at {serviceAddress}.");
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                host.Close();
            }

            Console.WriteLine("Goodbye.");
        }
    }
}
