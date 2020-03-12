using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Fahrzeug vehicle1 = new ObjectFactory<Fahrzeug>().CreateInstance();
            PKW vehicle2 = new ObjectFactory<PKW>().CreateInstance();
            LKW vehicle3 = new ObjectFactory<LKW>().CreateInstance();
        }
    }
}
