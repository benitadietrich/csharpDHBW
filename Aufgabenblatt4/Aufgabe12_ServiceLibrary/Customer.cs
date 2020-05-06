using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe12_ServiceLibrary
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        public bool isPremiumCustomer { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is Customer c) && ((this.FirstName == c.FirstName) && (this.LastName == c.LastName));
        }
    }
}
