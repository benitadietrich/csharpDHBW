using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Aufgabe7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SalesRepresentative> rep;
            var serializer = new XmlSerializer(typeof(List<SalesRepresentative>));

            using (var stream = new FileStream(@"vertreter.xml", FileMode.Open))
            {
                rep = serializer.Deserialize(stream) as List<SalesRepresentative>;
            }

            Console.WriteLine("\nListe aller Vertreter aus Baden-Württemberg, sortiert nach Unternehmen: ");
            GetSalesRepsFromBadenWuerttemberg(rep).ForEach(value => Console.WriteLine($"Name: {value.firstName} {value.lastName}, Firma: {value.company}, Umsatz: {value.salesVolume}"));
            Console.WriteLine("\nListe aller Vertreter, sortiert nach Unternehmen: ");
            GetSalesRepsGroupedByCompany(rep).ForEach(value => Console.WriteLine($"Name: {value.FirstName} {value.LastName}, Firma: {value.Company}, Umsatz: {value.SalesVolume}"));
            Console.WriteLine("\nListe der 10 Vertreter, die am wenigsten Umsatz machen: ");
            GetTopTenLosers(rep).ForEach(value => Console.WriteLine($"Name: {value.FirstName} {value.LastName}, Firma: {value.Company}, Umsatz: {value.SalesVolume}"));
            Console.ReadKey();
        
           }

        private static List<(string firstName, string lastName, string company, decimal salesVolume)> GetSalesRepsFromBadenWuerttemberg(List<SalesRepresentative> salesReps)
        {
            return (from salesRep in salesReps
                         where salesRep.Area == "Baden-Württemberg"
                         orderby salesRep.Company ascending
                         select (salesRep.FirstName, salesRep.LastName, salesRep.Company, salesRep.SalesVolume)).ToList();

        }
            
        private static List<SalesRepresentative> GetSalesRepsGroupedByCompany(List<SalesRepresentative> salesReps)
        {
            var temp = from salesRep in salesReps
                       where salesRep.SalesVolume > 10000
                       orderby salesRep.SalesVolume descending
                       group salesRep by salesRep.Company;

            List<SalesRepresentative> result = new List<SalesRepresentative>();
            foreach (var group in temp)
            {
                foreach (var rep in group)
                {
                    result.Add(rep);
                }
            }

            return result;
                    
        }

        private static List<SalesRepresentative> GetTopTenLosers(List<SalesRepresentative> salesReps)
        {
            return (from salesRep in salesReps
                          orderby salesRep.SalesVolume ascending
                          select salesRep).Take(10).ToList();
        }
    }
}