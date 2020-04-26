using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe9
{
    class Program
    {
        static void Main(string[] args)
        {

            var person = new Person();

            person.NameChanged += Console.WriteLine;

            person.FirstName = "Donna";
            person.LastName = "Summer";

            Console.ReadKey();

        }
    }
}
