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
    /// Holds the price,name,ticker, and can update the current price of the stock.
    /// </summary>
    public class Stock
    {
        /// <summary>
        /// Stock company name / identifier.
        /// </summary>
        private string _name;

        /// <summary>
        /// The current dollar value of the stock
        /// </summary>
        private double _currentprice;

        /// <summary>
        /// Company ticker
        /// </summary>
        private string _ticker;

        /// <summary>
        /// The price the stock was purchased at.
        /// </summary>
        private double _orginalprice;

        /// <summary>
        /// Creates a new stock with the name,ticker, and price.
        /// </summary>
        /// <param name="ticker">Ticker of the company</param>
        /// <param name="name">Type of stock, like company name</param>
        /// <param name="price">Dollar value of the stock</param>
        public Stock(string ticker, string name, double price)
        {
            _name = name;
            _currentprice = price;
            _ticker = ticker;
            _orginalprice = price;
        }

        /// <summary>
        /// Updates stocks value based on the current market.
        /// </summary>
        /// <param name="multiplier">The volatillity of the market.</param>
        public void UpdatePrice(double multiplier)
        {
            Random r = new Random();
            int i = r.Next(0,2); // 0 or 1
            double p = _currentprice * (multiplier / 100);
            if (i == 0)//Goes up
            {
                _currentprice += p;
            }
            else //Goes down
            {
                _currentprice -= p;
            }
            //Console.WriteLine("*" + p); // for testing
        }

        /// <summary>
        /// Returns the company name.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
        }

        /// <summary>
        /// returns the original value of the stock.
        /// </summary>
        public double OrginalPrice
        {
            get
            {
                return _orginalprice;
            }
        }

        /// <summary>
        /// Returns the current dollar value of the stock.
        /// </summary>
        public double CurrentPrice
        {
            get
            {
                return _currentprice;
            }
        }

        /// <summary>
        /// Returns the ticker apprivataiton.
        /// </summary>
        public string Ticker
        {
            get
            {
                return _ticker;
            }
        }
    }
}
