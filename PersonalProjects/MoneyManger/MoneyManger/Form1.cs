/* Author: Cody Riniker */
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

namespace MoneyManager
{
    public partial class Form1 : Form
    {
        //Allows user to save without having to manully select the file each time
        private string _quicksavepath = null;

        //Create Form
        public Form1()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Reads in file and returns the text in a list
        /// </summary>
        /// <param name="path">The full path to the file the user chose</param>
        /// <returns>A list with each line of text</returns>
        private List<string> ReadInFile(string path)
        {
            List<string> information = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    information.Add(sr.ReadLine());
                }
            }
            return information;
        }

        /// <summary>
        /// Opens the file browser dialog and allows the user to select a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> list = ReadInFile(uxOpenFileDialog.FileName);
                _quicksavepath = uxOpenFileDialog.FileName; //Update quicksave path
                uxListView.Items.Clear(); //Clear out items in listview
                uxTotalAmountLabel.Text = "0"; //Resets total when file is loaded in.
                UpdateListView(list);
            }
        }

        /// <summary>
        /// Save the information in the listbox to the file the user chose.
        /// The quicksave path will be updated if its not already set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxSaveMenuItem_Click(object sender, EventArgs e)
        {
            if (Equals(_quicksavepath, null))
            {
                if (uxSaveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Save(uxSaveFileDialog.FileName);
                    _quicksavepath = uxSaveFileDialog.FileName;
                }
            }
            else
            {
                Save(_quicksavepath);
            }
        }

        /// <summary>
        /// Open a file browser and allows the user to chose the file they want to save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uxSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Save(uxSaveFileDialog.FileName);
                _quicksavepath = uxSaveFileDialog.FileName; //Update quicksavepath
            }
        }

        /// <summary>
        /// Writes information in listview to a text file and deliminates it with a comma
        /// Used to handle saving for both Save and Save As menu items
        /// </summary>
        /// <param name="path">The full path the file the user wants to save to</param>
        private void Save(string path)
        {
            if (path.Split('.')[1] == "txt")
            { //Only allows uses to write to / over text files 
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (ListViewItem item in uxListView.Items)
                    {
                        string amount = item.Text;
                        string description = item.SubItems[1].Text;
                        sw.WriteLine(amount + "," + description); //Add comma deliminator
                    }
                }
            }
            else
            {
                MessageBox.Show("Error, Only save to .txt files");
                _quicksavepath = null;
            }
        }

        /// <summary>
        /// Adds information from text file to the listview
        /// </summary>
        /// <param name="information">A list containing each line of text from the file</param>
        private void UpdateListView(List<string> information)
        {
            try
            {
                double total = 0;
                for (int i = 0; i < information.Count; i++)
                {
                    double amount = Convert.ToDouble(information[i].Split(',')[0]);
                    total += amount;
                    string description = information[i].Split(',')[1];
                    ListViewItem item = new ListViewItem();
                    item.Text = amount.ToString(); //First column of list view is set to amount
                    item.SubItems.Add(description); //Second column of list view is set to description

                    if (amount < 0)
                    {
                        item.ForeColor = Color.Red; //Negative amount
                    }
                    else
                    {
                        item.ForeColor = Color.Blue; //Positive amount
                    }
                    UpdateTotalAmount(amount);
                    uxListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                _quicksavepath = null; //So user dosen't quick save over bad file
                uxListView.Items.Clear(); //Clear out items in listview
                uxTotalAmountLabel.Text = "0"; //Resets total when file is loaded in.
                MessageBox.Show("An error occured");
            }

        }

        /// <summary>
        /// Edits total amount by adding amount to current total
        /// </summary>
        /// <param name="amount">The amount of money to be added</param>
        private void UpdateTotalAmount(double amount)
        {
            double newtotal = Convert.ToDouble(uxTotalAmountLabel.Text) + amount;
            uxTotalAmountLabel.Text = string.Format("{0:N2}", newtotal);
            if (newtotal < 0)
            {
                uxTotalAmountLabel.ForeColor = Color.Red;
            }
            else
            {
                uxTotalAmountLabel.ForeColor = Color.Blue;
            }
        }

        /// <summary>
        /// Adds the amount and description from the text boxs to the listview
        /// Total label is also updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxAddButton_Click(object sender, EventArgs e)
        {
            double amount = Convert.ToDouble(uxAmountBox.Text);
            string description = uxDescriptionBox.Text;
            ListViewItem item = new ListViewItem();
            item.Text = amount.ToString();
            item.SubItems.Add(description);
            if (amount < 0)
            {
                item.ForeColor = Color.Red;
                item.SubItems[1].ForeColor = Color.Red;
            }
            else
            {
                item.ForeColor = Color.Blue;
                item.SubItems[1].ForeColor = Color.Blue;
            }
            uxListView.Items.Add(item);

            UpdateTotalAmount(amount);
        }

        /// <summary>
        /// Checks to see if string in uxAmountBox is a double and will enable the add button accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxAmountBox_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(uxAmountBox.Text, out double amount)) {
                uxAddButton.Enabled = true;
            }
            else
            {
                uxAddButton.Enabled = false;
            }
        }

        /// <summary>
        /// Allows used to press enter key instead of add button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && uxAddButton.Enabled)
            {
                uxAddButton_Click(sender,e);
            }
        }

    } 
}
