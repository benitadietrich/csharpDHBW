using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_3
{
    class LKW : Fahrzeug
    {
        public override string Drive()
        {
            return "LKW " + base.Drive();
        }
    }
}