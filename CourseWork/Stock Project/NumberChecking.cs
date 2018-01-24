/***********************
 * CIS 501
 * Author: Cody Riniker
 * Programing Assignment 1
************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker501
{
    /// <summary>
    /// Used to simplifiy checking if input is correct.
    /// </summary>
    public static class NumberChecking
    {
        /// <summary>
        /// Checks to make sure user input is an int.
        /// </summary>
        /// <returns>An int after parseing the user given string.</returns>
        public static int IntCheck()
        {
            string s = Console.ReadLine();
            int i = 0;
            bool check = false;
            while (check == false)
            {
                try
                {
                    i = Convert.ToInt32(s);
                    check = true;
                }
                catch (Exception ex)
                {
                    Console.Write("Error, Please enter in a integer value: ");
                    s = Console.ReadLine();
                }
            }
            return i;
        }

        /// <summary>
        /// Checks to make sure user input is a double.
        /// </summary>
        /// <returns>A double after parsing the user given string.</returns>
        public static double DoubleCheck()
        {
            string s = Console.ReadLine();
            double d = 0;
            bool check = false;
            while (check == false)
            {
                try
                {
                    d = Convert.ToDouble(s);
                    check = true;
                }
                catch (Exception ex)
                {
                    Console.Write("Error, Please enter in a double value: ");
                    s = Console.ReadLine();
                }
            }
            return d;
        }
    }
}
