﻿/* Author: Cody Riniker */
/* Description: This program is used to generate fake data for an sql database */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FakeSQLInformationGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Hide();
        }

        /// <summary>
        /// Reads in and parses data from Fake_Names.txt then writes the sql statments to another file.
        /// </summary>
        /// <param name="filepath">The file to write to.</param>
        public void GenerateData(string filepath)
        {
            int id = 0;
            Random r = new Random();

            //A couple of email sites that are used when generating fake users email accounts.
            string[] emailSites = { "@gmail.com", "@yahoo.com", "@MSN.com", "@hotmail.com" };

            //Used at end of program to write sql statments to text file.
            List<string> SQLStatements = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader("Fake_Names.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string info = sr.ReadLine();
                        string firstName = info.Split(' ')[0];
                        string lastName = info.Split(' ')[1];

                        //Create fake email by using last name and random email site
                        string email = lastName + emailSites[r.Next(0, emailSites.Length)];

                        //password is just a random string of numbers and letters generated by the for loop.
                        string password = "";
                        for (int i = 0; i < 10; i++)
                        {
                            int num = r.Next(0, 91);
                            if (num < 65 || num > 90)
                            {
                                password += num;
                            }
                            else
                            {
                                password += (char)num;
                            }
                        }

                        double money = r.NextDouble() * 1000000;

                        //Add the fake users information to list
                        SQLStatements.Add("INSERT INTO user_info VALUES(" + id + ",'" + firstName + "','" + lastName + "','" + email + "','" + password + "'," + money.ToString("F") + ");");
                        id++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured.");
            }

            //Write all fake users information to a text file which can the be inputed into a sql database
            using (StreamWriter sw = new StreamWriter(filepath))
            {
                for (int i = 0; i < SQLStatements.Count; i++)
                {
                    sw.WriteLine(SQLStatements[i]);
                }
            }
        }

        private void uxStartButton_Click(object sender, EventArgs e)
        {
            if (uxSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                GenerateData(uxSaveFileDialog.FileName);
                MessageBox.Show("Data was generated succesfully.");
            }
            Application.Exit();
        }
    }
}
