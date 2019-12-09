using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_Samples.Models
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
