using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aufgabe6
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee PetraMaier = new Employee
            {
                FirstName = "Petra",
                LastName = "Maier",
                Gender = Gender.Female
            };

            var serializer = new XmlSerializer(typeof(Employee));

            using (var stream = new FileStream(@"C:\Users\DietrichB\source\repos\csharpDHBW\Aufgabenblatt2\Aufgabe6\PetraMaier.xml", FileMode.Create))
            {
                serializer.Serialize(stream, PetraMaier);
            }

            Employee emp;

            var serializer2 = new XmlSerializer(typeof(Employee));

            using (var stream = new FileStream(@"C:\Users\DietrichB\source\repos\csharpDHBW\Aufgabenblatt2\Aufgabe6\PetraMaier.xml", FileMode.Open))
            {
                emp = serializer2.Deserialize(stream) as Employee;
            }

            Console.WriteLine("Vorname: " + emp.FirstName + ", Nachname: " + emp.LastName + ", Geschlecht: " + emp.Gender);
            Console.ReadKey();


        }
    }
}
