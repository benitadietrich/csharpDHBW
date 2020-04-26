using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aufgabe7
{
    [XmlType("Vertreter")]
    public class SalesRepresentative
    {
        [XmlElement("Vorname")]
        public string FirstName { get; set; }

        [XmlElement("Nachname")]
        public string LastName { get; set; }

        [XmlElement("Firma")]
        public string Company { get; set; }

        [XmlElement("Gebiet")]
        public string Area { get; set; }

        [XmlElement("Umsatz")]
        public decimal SalesVolume { get; set; }
    }
}
