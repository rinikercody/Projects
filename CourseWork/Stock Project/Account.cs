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
    /// This manages the one and only account, it handles withdrawls,deposits and managing money after purchasing or selling of stocks.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Holds all 3 portfolios.
        /// </summary>
        List<Portfolio> _portfolios = new List<Portfolio>();

        /// <summary>
        /// Keeps track of how many active portfolios there are.
        /// </summary>
        private int _numberOfPortfolios = 0;

        /// <summary>
        /// The cash on hand.
        /// </summary>
        private double _cash;

        /// <summary>
        /// Flat fee for withdrawl or deposit.
        /// </summary>
        const double TRANSFER_FEE = 4.99;

        /// <summary>
        /// Flat fee per trade.
        /// </summary>
        const double TRADE_FEE = 9.99;

        /// <summary>
        /// Current portfolio being managed.
        /// </summary>
        private int _current = 0;

        /// <summary>
        /// used to track gain or loss for portfolios that have been deleted.
        /// </summary>
        private double _deleted_gain_loss = 0;

        /// <summary>
        /// Creates 3 default portfolios.
        /// </summary>
        public void Intialize()
        {
            for(int i = 0; i < 3; i++)
            {
                _portfolios.Add(new Portfolio());
            }
        }

        /// <summary>
        /// Used to handle the creation and deletion of portfolios.
        /// </summary>
        public void PortfolioManagement()
        {
            Console.WriteLine("Would you like to create or delete a portfolio?");
            Console.WriteLine("  1. Create");
            Console.WriteLine("  2. Delete");
            Console.Write("Enter choice(1 or 2): ");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                if (_numberOfPortfolios == 3)
                {
                    Console.WriteLine("Sorry, the maximum number of portfolios have already been made.");
                }
                else
                {
                    Console.Write("Enter a name for the portfolio: ");
                    string name = Console.ReadLine();

                    for (int i = 0; i < 3; i++)
                    {
                        if (_portfolios[i].AccountName == name)
                        {
                            Console.WriteLine("There is already a portfolio with that name.");
                            return;
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if ((_portfolios[i].AccountName == "EmptyPortfolio"))
                        {
                            _portfolios[i] = new Portfolio(name);
                            _current = i;
                            _numberOfPortfolios++;
                            Console.WriteLine("Portfolio " + name + " was created.");
                            Console.WriteLine("You are now managing the portfolio " + name + ".");
                            break;
                        }
                    }
                    
                }
            }
            else if (answer == "2")
            {
                Console.Write("Enter the name of the portfolio you want to delete: ");
                string name = Console.ReadLine();
                bool found = false;

                for (int i = 0; i < 3; i++)
                {
                    if (name == _portfolios[i].AccountName)
                    {
                        found = true;
                        _cash += (_portfolios[i].SellAllStocks() - TRADE_FEE);
                        _deleted_gain_loss += _portfolios[i].GainLoss;
                        
                        Console.WriteLine(name + " was deleted.");

                        if (_portfolios[i].AccountName == _portfolios[_current].AccountName)
                        {
                            Console.WriteLine("You will need to switch to another portfolio or create a new one before you can trade stocks again. ");
                        }
                        _portfolios[i] = new Portfolio(); //deletes account and creates an empty one.
                        _numberOfPortfolios--;
                        return;
                    }
                }
                if (found == false)
                {
                    Console.WriteLine("Portfolio entered was not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        /// <summary>
        /// Puts money into the account.
        /// </summary>
        /// <param name="amount">Amount to put in</param>
        public void Deposit(double amount)
        {
            _cash += amount;
            _cash -= TRANSFER_FEE;
            Console.WriteLine("Deposit was successful.");
        }

        /// <summary>
        /// Takes money / sells stocks from the account
        /// </summary>
        /// <param name="amount">Ammount to take</param>
        /// <returns>Amount withdrawn</returns>
        public double Withdraw(double amount)
        {
            double postion_total = 0;
            for(int i = 0; i < 3; i++)
            {
                postion_total += _portfolios[i].ViewAllStocks(); //Returns the amount from selling all stock in all three portfolios.
            }

            if (amount > _cash && amount < _cash + postion_total) // if there is not enough money but can sell stocks to get enough.
            {
                Console.WriteLine("Amount entered is larger then the amount of cash in you're account");
                Console.WriteLine("Would you like to sell stocks to complete this transaction?:");
                Console.WriteLine("  1. Yes");
                Console.WriteLine("  2. No");
                Console.Write("Enter choice(1 or 2): ");
                string s = Console.ReadLine();
                if (s == "1")
                {
                    Console.Write("Chose the portfolilo you want to sell postions from: ");
                    string p_name = Console.ReadLine();
                    for (int i = 0; i < 3; i++)
                    {
                        if (p_name == _portfolios[i].AccountName)
                        {
                            SellStock();
                            _cash -= TRANSFER_FEE;
                            _cash -= amount;
                            Console.WriteLine("Withdrawl successful.");
                            return 0;
                        }
                    }
                    Console.WriteLine("Portfolio specified was not found.");
                }
                else if (s == "2")
                {
                    Console.WriteLine("Withdrawl canceled.");
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                    Console.WriteLine("Withdrawl canceled.");
                }
            }
            else if (amount > (postion_total + _cash)) //cant afford no matter what
            {
                Console.WriteLine("You don't have enough funds.");
                return 0;
            }
            else
            {
                Console.WriteLine("Withdrawl successful.");
                _cash -= amount;
                _cash -= TRANSFER_FEE;
            }
            return amount;
        }

        /// <summary>
        /// Buys stock depending on the method.
        /// </summary>
        /// <param name="sm">All stocks that can be bought.</param>
        /// <param name="method">Buying by amount vs by cash.</param>
        public void BuyStock(Dictionary<string,Stock> sm, int method)
        {
            if (_numberOfPortfolios > 0 && _portfolios[_current].AccountName != "EmptyPortfolio")
            {
                Console.Write("Enter the ticker for the company you want to buy stocks of: ");
                string tic = Console.ReadLine();
                tic = tic.ToUpper();
                if (method == 1)
                {
                    Stock s;
                    if (sm.TryGetValue(tic, out s))
                    {
                        Console.Write("Enter the number of stocks you want to purchase: ");
                        double d = NumberChecking.DoubleCheck();
                        if ((d * s.CurrentPrice) < _cash)
                        {
                            _portfolios[_current].AddStocks(s, d);
                            _cash -= TRADE_FEE;
                            _cash -= (d * s.CurrentPrice);
                            Console.WriteLine(d + " stocks of " + s.Name + " were purchased.");
                        }
                        else
                        {
                            Console.WriteLine("You don't have enough money to buy that many stocks.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Company was not found.");
                    }
                }
                else
                {
                    Stock s;
                    if (sm.TryGetValue(tic, out s) == false)
                    {
                        Console.WriteLine("Company specified was not found on the stock market.");
                        return;
                    }
                    Console.WriteLine("Enter the amount of money you want to spend.");
                    Console.Write("$");
                    double d = NumberChecking.DoubleCheck();
                    if (d > _cash)
                    {
                        Console.WriteLine("You don't have that much money");
                        return;
                    }
                    int num_of_stocks = 0;

                    for (double c = d; s.CurrentPrice < c; c -= s.CurrentPrice)
                    {
                        _portfolios[_current].AddStocks(s, 1);
                        num_of_stocks++;
                        _cash -= s.CurrentPrice;
                    }
                    _cash -= TRADE_FEE;
                    Console.WriteLine("Number of " + s.Name + " stocks purchased was " + num_of_stocks);
                }
            }
            else
            {
                Console.WriteLine("You need to create a portfolio before you can trade stocks.");
            }
        }

        /// <summary>
        /// Method used to sell stock. Adds money gained from selling to overall cash after transaction.
        /// </summary>
        public void SellStock()
        {
            double total = 0;
            if (_numberOfPortfolios > 0 && _portfolios[_current].AccountName != "EmptyPortfolio")
            {
                Console.Write("Enter the ticker of the stock you want to sell: ");
                string tic = Console.ReadLine();

                List<Stock> list = _portfolios[_current].ViewStocks;
                bool found = false;

                for (int i  = 0; i < list.Count; i++ )
                {
                    if (tic.ToUpper() == list[i].Ticker)
                    {
                        found = true;
                        Console.Write("How many stocks of " + list[i].Name + " would you like to sell?: ");
                        int num_of_stocks = 0; //Actual number of user chosen stock in the portfolio.
                        int num = NumberChecking.IntCheck(); // Number the user choses to sell.

                        //This loop counts up all the stocks of the company chosen.
                        for(int j = 0; j < list.Count; j++)
                        {
                            if(list[j].Ticker == tic.ToUpper())
                            {
                                num_of_stocks++;
                            }
                        }

                        if (num <= num_of_stocks) //Actually have the stocks to sell.
                        {
                            Stock st = list[i];
                            total += _portfolios[_current].RemoveStocks(list[i], num);
                            Console.WriteLine(num + " stocks of " + st.Name + " were sold for a total of " + string.Format("{0:0.00}", (st.CurrentPrice * num)));
                            _cash += total;
                            _cash -= TRADE_FEE;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You don't own that many stocks.");
                            break;
                        }
                    }
                }
                if(found == false)
                {
                    Console.WriteLine("Company entered was not found in portfolio.");
                }
            }
            else
            {
                Console.WriteLine("You need to create a portfolio before you can trade stocks.");
            }    
        }

        /// <summary>
        /// Switchs the portfolio to the one specified. 
        /// </summary>
        /// <param name="name">Portfolio to switch to.</param>
        public void SwitchPortfolio(string name)
        {
            for(int i = 0; i < 3; i++)
            {
                if(name == _portfolios[i].AccountName)
                {
                    _current = i;
                    Console.WriteLine("Portfolio switched. You are now managing: " + _portfolios[_current].AccountName);
                    return;
                }
            }
            Console.WriteLine("Can't find the portfolio specified.");
        }

        /// <summary>
        /// Gets the total number of postions across all 3 portfolios.
        /// </summary>
        /// <returns></returns>
        public double GetAllPostions(string method)
        {
            if (_current != -1)
            {
                if (method == "all")
                {
                    double total = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        total += _portfolios[i].ViewAllStocks();
                    }
                    return total;
                }
                return _portfolios[_current].ViewAllStocks();
            }
            else
            {
                return 0; // no portfolios created yet 
            }
        }

        /// <summary>
        /// Gets the percentage of each stock in the current portfolio.
        /// </summary>
        public void StockPercentage()
        {
            _portfolios[_current].GetStockPercentage();
            Console.WriteLine("Total money in stocks of " + _portfolios[_current].AccountName + " is $" + string.Format("{0:0.00}", _portfolios[_current].ViewAllStocks()));
        }

        /// <summary>
        /// Gets postion percentage of current portfolio.
        /// </summary>
        /// <returns></returns>
        public double PostionPercentage()
        {
            return (_portfolios[_current].ViewAllStocks() / GetAllPostions("all")) * 100;
        }

        /// <summary>
        /// Gets the gain/loss report for the account.
        /// </summary>
        /// <returns></returns>
        public double GainLossReport(string method)
        {
            if (method == "all") //Total gain/loss
            {
                double total = 0;
                for (int i = 0; i < 3; i++)
                {
                    total += _portfolios[i].GainLoss;
                    
                }
                total += _deleted_gain_loss;
                return total;
            }
            return _portfolios[_current].GainLoss; // This portfolios gain/loss
        }

        /// <summary>
        /// Returns the cash on hand
        /// </summary>
        public double Cash
        {
            get
            {
                return _cash;
            }
        }

        /// <summary>
        /// Returns the name of the current portfolio.
        /// </summary>
        public string CurrentPortfolio
        {
            get
            {
             return _portfolios[_current].AccountName;  
            }
        }

    } // end class
}
