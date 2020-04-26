using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe02
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen zu Aufgabe 2");
            Boolean exit = false;
            while (exit == false)
            {
                String input = Console.ReadLine();
                switch (input)
                {
                    case "zählen":
                        Console.WriteLine("Geben Sie einen minimalen Wert ein:");
                        int min = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Geben Sie einen maximalen Wert ein:");
                        int max = Convert.ToInt32(Console.ReadLine());
                        for (var i = min; i <= max; i++)
                        {
                            Console.WriteLine("Aktueller Wert " + i);
                        }
                        break;

                    case "beenden":
                        exit = true;
                        break;

                    case "hilfe":
                        Console.WriteLine("\n-----------------------\n" + "Es existieren folgende Kommandos:\n"
                            + "- zählen\n" + "- beenden\n" + "- hilfe\n" + "\n-----------------------\n");
                        break;

                }
            }
        }
    }
}
