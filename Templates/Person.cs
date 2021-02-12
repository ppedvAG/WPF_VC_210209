using System;
using System.Collections.Generic;
using System.Text;

namespace Templates
{
    //Bsp-Klasse
    public class Person
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public int Alter { get; set; }

        public override string ToString()
        {
            return $"Person: {Nachname}";
        }
    }
}
