using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
    [DataContract]
    public class BusinessUnit
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataContractFormat]
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is BusinessUnit))
            {
                return false;
            }
            return (this.Name == ((BusinessUnit)obj).Name)
                && (this.Description == ((BusinessUnit)obj).Description);
        }

    }

}
