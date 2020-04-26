using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe9
{

    public delegate void NameChanged(object sender, EventArgs e);
    class Person
    {
        public event Action<string> NameChanged;

        public String _firstname;
        public String _lastname;
        public String FirstName
        {
            get => _firstname;
            set
            {
                _firstname = value;
                NameChanged?.Invoke($"Vorname: {_firstname}, Nachname: {_lastname}");
            }
        }
        public String LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                NameChanged?.Invoke($"Vorname: {_firstname}, Nachname: {_lastname}");
            }
        }
    }
}
