using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe8
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee herbertMueller = new Employee()
            {
                FirstName = "Herbert",
                LastName = "Müller",
                Gender = Gender.Male
            };

            Employee employee;

            var formatter = new BinaryFormatter();
            var stream = new MemoryStream();

            using (stream)
            {
                formatter.Serialize(stream, herbertMueller);
                stream.Position = 0;
                employee = formatter.Deserialize(stream) as Employee;
            }

            Console.WriteLine($"Original: Vorname: {herbertMueller.FirstName}, Nachname: {herbertMueller.LastName}, Geschlecht: {herbertMueller.Gender}");
            Console.WriteLine($"Klon: Vorname: {employee.FirstName}, Nachname: {employee.LastName}, Geschlecht: {employee.Gender}");
            Console.ReadKey();
        


        }
    }
}
