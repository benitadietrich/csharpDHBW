using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aufgabe13.Models
{
    public class Customer
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; } = new Address();
        public int Id { get; set; }
    }
}
