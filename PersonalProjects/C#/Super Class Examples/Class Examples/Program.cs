/* Author: Cody Riniker */
/* This is a simple program that uses classes and superclasses. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //How users animals are stored
            List<Animal> farm = new List<Animal>();

            bool outerLoopCheck = true;
            while (outerLoopCheck)
            {
                bool innerLoopCheck = true;
                while (innerLoopCheck)
                {
                    Console.WriteLine("Please choose and animal to add to your farm: ");
                    Console.WriteLine("1. Cat");
                    Console.WriteLine("2. Dog");
                    Console.WriteLine("3. Fish");
                    string str = Console.ReadLine();
                    string name;
                    switch (str)
                    {
                        case "1":
                            Console.WriteLine("Please give you're cat a name: ");
                            name = Console.ReadLine();
                            farm.Add(new Cat(name));
                            break;
                        case "2":
                            Console.WriteLine("Please give you're dog a name: ");
                            name = Console.ReadLine();
                            farm.Add(new Dog(name));
                            break;
                        case "3":
                            Console.WriteLine("Please give you're fish a name: ");
                            name = Console.ReadLine();
                            farm.Add(new Fish(name));
                            break;
                        default:
                            innerLoopCheck = false;
                            break;
                    }
                } //End inner loop

                foreach (Animal a in farm)
                {
                    Console.WriteLine(a.Species + " " + a.Name + " " + a.Age);
                    a.IncreaseAge();
                }
                Console.Write("Add more animals(y/n): ");
                string ans = Console.ReadLine();
                if (ans != "y") outerLoopCheck = false;
            } //End outer loop
            Console.ReadLine();
        }
    }
}
