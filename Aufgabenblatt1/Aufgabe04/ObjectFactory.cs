using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe_4
{
    class ObjectFactory<T> where T: class, new()
    {
        public T CreateInstance()
        {
            return new T();
        }
    }
}
