using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_5
{
    public static class Program
    {
        static void Main(string[] args)
        {
            string a = "Es ist heute ein sehr schöner Tag in Horb am Neckar.";
            string b = "Diese Zeichenkette ist nicht lang, denke ich.";
            string c = "Tabulatoren\tsind auch Leerzeichen";

            Console.WriteLine("a: " + a.CountWords());
            Console.WriteLine("b: " + b.CountWords());
            Console.WriteLine("c: " + c.CountWords());
        }

        static int CountWords(this string str)
        {
            int count = 0;
            foreach (String s in str.Split(' ', '\t'))
                count++;

            return count;

        }


    }
}
