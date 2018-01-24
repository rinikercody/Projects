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
using System.IO;

namespace Ticker501
{
    /// <summary>
    /// Main menu for the program. Manages the one and only account and displays info.
    /// The Ticker.txt file needs to be in the same directory as the Ticker501 application file.
    /// </summary>
    class Menu
    {
        /// <summary>
        /// Hold all the companies that were read in as stock.
        /// </summary>
        private static Dictionary<string, Stock> _stockmarket = new Dictionary<string, Stock>();
    
        /// <summary>
        /// The main menu for the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ReadInFile();

            Account a = new Account(); // Only account
            a.Intialize();

            a.PortfolioManagement(); // creates or deletes a portfolio when program is started.

            while (true) //Go through options until you want to exit.
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("  1. Deposit money into account"); // add funds
                Console.WriteLine("  2. Withdrawl money from account"); //withdrawl money
                Console.WriteLine("  3. Buy stock"); // buy stock
                Console.WriteLine("  4. Sell stock"); // sell stock
                Console.WriteLine("  5. Switch portfolio"); // switch to another portfolio that is already made
                Console.WriteLine("  6. View cash and postions balance"); // Cash and postions balance
                Console.WriteLine("  7. View postions percentage");//show percent and dollar value of stock in portfolio
                Console.WriteLine("  8. View gain/loss reports"); //in $
                Console.WriteLine("  9. Update stock prices"); //upadates stock prices based on volitilty
                Console.WriteLine("  10. Create or delete a portfolio"); // create or delete a portfolio
                Console.WriteLine("  11. Exit"); // exit program
                Console.Write("Enter your choice(1 - 11): ");
                string s = Console.ReadLine();
                if (s == "1")
                {
                    Console.Write("Enter amount you want to deposit: ");
                    a.Deposit(NumberChecking.DoubleCheck());
                }
                else if (s == "2")
                {
                    Console.Write("Enter amount you want to withdrawl: ");
                    a.Withdraw(NumberChecking.DoubleCheck());
                }
                else if (s == "3")
                {
                    Console.WriteLine("  1. Enter number of stocks to purchase.");
                    Console.WriteLine("  2. Enter the dollar amount you want to spend on stocks.");
                    Console.Write("Choose method(1 or 2): ");
                    string str = Console.ReadLine();
                    if ( str == "1")
                    {
                        a.BuyStock(_stockmarket,1); // buy by number
                    }
                    else if(str == "2")
                    {
                        a.BuyStock(_stockmarket, 2); // buy based on money
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice");
                    }
                }
                else if (s == "4")
                {
                    a.SellStock();
                }
                else if (s == "5")
                {
                    Console.Write("Enter the portfolio you want to switch to: ");
                    string ans = Console.ReadLine();
                    a.SwitchPortfolio(ans);
                }
                else if (s == "6")
                {
                    double total = 0;
                    Console.WriteLine("What would you like to view.");
                    Console.WriteLine("  1. Current portfolio cash and postions.");
                    Console.WriteLine("  2. All cash and all postions.");
                    Console.Write("Enter choice(1 or 2): ");
                    string ans = Console.ReadLine();
                    if (ans == "1")
                    {
                        total += a.Cash;
                        total += a.GetAllPostions("current"); //just this portfolio
                        Console.WriteLine("Total cash plus positions in current portfolio is $" + string.Format("{0:0.00}", total));
                    }
                    else if(ans == "2")
                    {
                        total += a.Cash;
                        total += a.GetAllPostions("all"); //all three portfolios
                        Console.WriteLine("Total cash and postions in account is $" + total);
                        if (a.CurrentPortfolio != "EmptyPortfolio")
                        {
                            Console.WriteLine(string.Format("{0:0.00}",a.CurrentPortfolio + " is %" + (a.GetAllPostions("current") / a.GetAllPostions("all"))) + " of entire account.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                else if (s == "7")
                {
                    Console.WriteLine("What would you like to view?");
                    Console.WriteLine("  1. Current portfolio's percentage of all portfolios.");
                    Console.WriteLine("  2. The percentage of each stock in " + a.CurrentPortfolio);
                    Console.Write("Enter choice(1 or 2): ");
                    string ans = Console.ReadLine();
                    if (ans == "1")
                    {
                        Console.WriteLine("Current portfolio percentage of all postions is %" + string.Format("{0:0.00}", a.PostionPercentage()) + ".");
                    }
                    else if(ans == "2")
                    {
                        a.StockPercentage();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                else if (s == "8")
                {
                    Console.WriteLine("What would you like to view.");
                    Console.WriteLine("  1. Current portfolio gain/loss report.");
                    Console.WriteLine("  2. Total gain/loss report.");
                    Console.Write("Enter choice(1 or 2): ");
                    string ans = Console.ReadLine();
                    if(ans == "1")
                    {
                        Console.WriteLine("Here is your total gain/loss for " + a.CurrentPortfolio + " $" + string.Format("{0:0.00}", a.GainLossReport("current")));
                    }
                    else if (ans == "2")
                    {
                        Console.WriteLine("Here is your total gain/loss: $" + string.Format("{0:0.00}", a.GainLossReport("all")));
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                else if (s == "9")
                {
                    RunSimulation(); 
                }
                else if (s == "10")
                {
                    a.PortfolioManagement();
                }
                else if (s == "11")
                {
                    Environment.Exit(0);
                }
                else if(s == "cash")
                {
                    //For testing
                    Console.WriteLine(a.Cash);
                }
                else
                {
                    Console.WriteLine("Invalid input. Enter in a number 1 - 11.");
                }
            }
        }

        /// <summary>
        /// Reads in info from file and puts it into the stock market.
        /// </summary>
        private static void ReadInFile()
        {
            string[] info = new string[3];
            double price;
            try
            {
                using(StreamReader sr = new StreamReader("Ticker.txt"))
                {
                    while (!sr.EndOfStream)
                    {

                        info = sr.ReadLine().Split('-');
                        if (info[0] != "")
                        {
                            price = Convert.ToDouble(info[2].Substring(1));
                            Stock s = new Stock(info[0], info[1], price);
                            _stockmarket.Add(info[0], s);
                        }

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not find the file Ticker.txt, it needs to be in the same directory as the Ticker501 application file.");
            }
        }

        /// <summary>
        /// Changes the price based on the user selected volitily of the market.
        /// </summary>
        private static void RunSimulation()
        {
            Random r = new Random();
            int i  = 0;
            Console.WriteLine("Chose the volatility of the market.");
            Console.WriteLine("  1. High");
            Console.WriteLine("  2. Medium");
            Console.WriteLine("  3. Low");
            Console.Write("Enter (1-3): ");
            string v = Console.ReadLine();
            if(v == "1")
            {
               i = r.Next(3, 15);
            }
            else if(v == "2")
            {
                i = r.Next(2, 8);
            }
            else if(v == "3")
            {
                i = r.Next(1, 4);
            }
            else
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            foreach(Stock st in _stockmarket.Values)
            {
                st.UpdatePrice(i);
            }
            Console.WriteLine("Market was updated.");
        }
    } // end class
} // end namespace
