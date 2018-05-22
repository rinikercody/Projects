using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Examples
{
    public class Dog : Animal
    {
        public Dog(string name)
        {
            Name = name;
            Species = "Dog";
        }

        public void Bark()
        {
            //Only the dog would be able to bark
        }
    }
}
