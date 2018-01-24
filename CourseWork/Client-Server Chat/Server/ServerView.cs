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

namespace FinalProject_T10_A_Server
{
    public partial class ServerView : Form
    {
        public ServerView()
        {
            InitializeComponent();
            ChannelListBox.Items.Add("Channel 1");
        }

        /// <summary>
        /// Displays users in the "Users" list box
        /// </summary>
        /// <param name="l">A list of users in the channel</param>
        public void showUsers(List<User> l)
        {
            UserListBox.Items.Clear();
            foreach (User u in l)
            {
                ContactListBox.Items.Add(u);
            }
        }

        /// <summary>
        /// Shows the contacts for the selected user in the "User's Contacts" list box
        /// </summary>
        /// <param name="u">The user whose contacts the list box will contain</param>
        public void showContacts(User u)
        {
            ContactListBox.Items.Clear();
            foreach (User contact in u.Contacts)
            {
                ContactListBox.Items.Add(contact.Name);
            }
        }

        /// <summary>
        /// Shows the users in the channel in the "Users in Channel" list box
        /// </summary>
        /// <param name="l">A list of the users in the channel</param>
        public void showChannelUsers(List<User> l)
        {
            ChannelUsersList.Items.Clear();
            foreach (User u in l)
            {
                ChannelUsersList.Items.Add(u);
            }
        }

        private void UserListBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }
    }
}
