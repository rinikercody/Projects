using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Examples
{
    public class Fish : Animal
    {
        public Fish(string name)
        {
            Name = name;
            Species = "Fish";
        }

        public void Swim()
        {
            //The fish would be the only one with the ability to swim.
        }
    }
}
