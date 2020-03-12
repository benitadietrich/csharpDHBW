using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_3
{
    class Program
    {
        static void Main(string[] args)
        {
                /* Fehler: Springt nie in die Drive Methode von LKW, wieso?*/

                Fahrzeug A = new Fahrzeug
                {
                    Kennzeichen = "KA TE 4711"
                };

                PKW B = new PKW
                {
                    Kennzeichen = "KA SC 1894"
                };

                LKW C = new LKW
                {
                    Kennzeichen = "S OS 2341"
                };

                Fahrzeug[] fahrzeuge = { A, B, C };

                foreach (Fahrzeug vehicle in fahrzeuge)
                {
                    Console.WriteLine(vehicle.Drive());
                }

                Console.Read();

           
        }
    }
}
