using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalProject_T10_A_ClassLibrary;

namespace ChatClient
{
    public partial class ContactListForm : Form
    {
        private Operation _operation;

        private string _contact;

        public ContactListForm()
        {
            InitializeComponent();
        }

        public void addButton_Click(object sender, EventArgs e)
        {
            
            AddContactForm addform = new AddContactForm();
            if (addform.ShowDialog() == DialogResult.OK)
            {
                _operation = Operation.AddContact;
                _contact = addform.GetContactToAdd;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void chatButton_Click(object sender, EventArgs e)
        {
            _operation = Operation.CreateChat;
            this.DialogResult = DialogResult.OK;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            _operation = Operation.RemoveContact;
            this.DialogResult = DialogResult.OK;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            _operation = Operation.LogOff;
            this.DialogResult = DialogResult.OK;
        }

        public Operation GetOperation
        {
            get
            {
                return _operation;
            }
        }

        public void UpdateListView(List<string> contacts)
        {
            foreach(string s in contacts)
            {
                listView1.Items.Add(s);
            }
        }

        public string GetContact
        {
            get
            {
                return _contact;
            }
        }
    }
}
