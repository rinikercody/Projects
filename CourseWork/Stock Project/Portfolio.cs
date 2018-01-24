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
    /// Holds all the stocks of one portfollio and manages them / displays portfolio specific information.
    /// </summary>
    public class Portfolio
    {
        /// <summary>
        /// Total number of stocks held in portfolio.
        /// </summary>
        private double _postions;

        /// <summary>
        /// Portfollio name / identifier
        /// </summary>
        private string _portfolioName;

        /// <summary>
        /// All stocks held in this portfolio.
        /// </summary>
        private List<Stock> _allStocks = new List<Stock>();

        /// <summary>
        /// Total gains or loss
        /// </summary>
        private double _gl = 0;

        /// <summary>
        /// Creates a default portfollio used when deleting a current portfollio or for finding space for a new one.
        /// </summary>
        public Portfolio()
        {
            _portfolioName = "EmptyPortfolio";
            _postions = 0;
        }

        /// <summary>
        /// Creates a new portfollio with the given name.
        /// </summary>
        /// <param name="n">portfolio indentifier</param>
        public Portfolio(string n)
        {
            _portfolioName = n;
        }

        /// <summary>
        /// Generates the gain/loss report based on stock prices.
        /// </summary>
        /// <param name="s">Stock to check the difference in price of.</param>
        public void GLReport(Stock s)
        {
            _gl += (s.CurrentPrice - s.OrginalPrice);
        }

        /// <summary>
        /// Adds stock to current portfollio.
        /// </summary>
        /// <param name="s">Stock to be added</param>
        /// <param name="num">Number of stocks to be added</param>
        public void AddStocks(Stock s, double num)
        {
            for (double i = 0; i < num; i++)
            {
                _allStocks.Add(s);
                _postions++;
            }
        }

        /// <summary>
        /// Sells and removes stocks from portfolio.
        /// </summary>
        /// <param name="s">Stock to sell</param>
        /// <param name="num">Amount to sell</param>
        /// <returns>The money gained from selling the stock</returns>
        public double RemoveStocks(Stock s, int num)
        {
            double d = 0;
            int sold = 0;
            int count = 0;
            for(int i = 0; i < _allStocks.Count; i++)
            {
                    if (_allStocks[i].Ticker.ToUpper() == s.Ticker)
                    {
                    count++;
                    } 
            }

            for(int j = 0; j < count && j < num; j++)
            {
                GLReport(s);
                d += s.CurrentPrice;
                _allStocks.Remove(s);
                _postions--;
            }
            return d;
        }
        /// <summary>
        /// Gets total price from selling all stocks without actually selling stocks
        /// </summary>
        /// <returns>Total from selling all stocks</returns>
        public double ViewAllStocks()
        {
            double total = 0;
            foreach(Stock s in _allStocks)
            {
                total += s.CurrentPrice;
            }
            return total;
        }

        /// <summary>
        /// Goes through current portfolio and shows percentage of each company in portfolio.
        /// </summary>
        public void GetStockPercentage()
        {
            Console.WriteLine("The percentage of each stock in " + _portfolioName);
            List<string> check = new List<string>();
            
            foreach(Stock s in _allStocks)
            {
                bool b = false;
                for (int j = 0; j < check.Count; j++)
                {
                    if(check[j] == s.Ticker)
                    {
                        b = true;
                    }
                }
                if (b == false)
                {
                    double i = NumberOfStocks(s.Ticker);
                    double p = i * s.CurrentPrice;
                    double c = (p / ViewAllStocks()) * 100;
                    check.Add(s.Ticker);
                    Console.WriteLine("The total amount of " + s.Name + " is $" + string.Format("{0:0.00}", p) + " and is %" + string.Format("{0:0.00}", c) + " of portfolio.");
                }
            }
        }

        /// <summary>
        /// Gets the number of stocks of the company.
        /// </summary>
        /// <param name="tic">The company ticker</param>
        /// <returns></returns>
        public double NumberOfStocks(string tic)
        {
            double i = 0;
            foreach(Stock s in _allStocks)
            {
                if (s.Ticker == tic.ToUpper())
                {
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Sells all the stocks. used when deleting portfolios.
        /// </summary>
        /// <returns>Total earned from selling all stocks</returns>
        public double SellAllStocks()
        {
            double total = 0;
            foreach( Stock s in _allStocks)
            {
                total += s.CurrentPrice;
                _gl += s.CurrentPrice;
                _gl -= s.OrginalPrice;
            }
            _postions = 0;
            return total;
        }

        /// <summary>
        /// Returns total postions
        /// </summary>
        public double Postions
        {
            get
            {
                return _postions;
            }
        }

        /// <summary>
        /// Returns the portfolio identifier.
        /// </summary>
        public string AccountName
        {
            get
            {
                return _portfolioName;
            }
        }

        /// <summary>
        /// Returns all stocks in the portfolio.
        /// </summary>
        public List<Stock> ViewStocks
        {
            get
            {
                return _allStocks;
            }
        }

        /// <summary>
        /// Returns the total gain or lose for this portfolio.
        /// </summary>
        public double GainLoss
        {
            get
            {
                return _gl;
            }
        }

    }
}
