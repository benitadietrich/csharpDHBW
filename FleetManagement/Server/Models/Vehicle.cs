using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    [DataContract]
    public class Vehicle
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string LicensePlate { get; set; }

        [DataMember]
        public string Brand { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public float Insurance { get; set; }

        [DataMember]
        public DateTime LeasingFrom { get; set; }

        [DataMember]
        public DateTime LeasingTo { get; set; }

        [DataMember]
        public float LeasingRate { get; set; }

    }
}
