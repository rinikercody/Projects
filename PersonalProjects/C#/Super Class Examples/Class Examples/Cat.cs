using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Examples
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            Name = name;
            Species = "Mammal";
        }

        public void Meow()
        {
            //Only A cat can access this method even though it is an animal.
        }
    }
}
