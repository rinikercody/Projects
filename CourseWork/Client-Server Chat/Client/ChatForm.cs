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
    public partial class ChatForm : Form, ClientViewManager
    {
        private ContactListForm _contactform;

        private string _message;

        private string _username;

        private Message _InputHandler;

        public ChatForm(string username, Message newMessageHandler)
        {
            InitializeComponent();
            _username = username;
            Text = "Chat: " + username; // Adds name to form box
            _InputHandler = newMessageHandler;
            messageTextBox.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _message = messageTextBox.Text;
                    UpdateListView(_message);
                    if (_InputHandler(new ClientMessage(Operation.Message, new KeyValuePair<string, string>(username,_message)))) 
                    {
                        messageTextBox.Text = "";
                    }
                }
            };
            messageTextBox.Select();
        }
        
        public bool MessageReceived(Operation message)
        {
            Invoke(new Action(() => messageListBox.TopIndex = messageListBox.Items.Add(message)));
            return true;
        }
        
        
        public void ViewContactsButton(object sender, EventArgs e)
        {
            _contactform = new ContactListForm();
            if (_contactform.ShowDialog() == DialogResult.OK)
            {
                 _message = _contactform.GetContact;
                 _InputHandler(new ClientMessage(_contactform.GetOperation, new KeyValuePair<string, string>(_username,_message)));
            }
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            _InputHandler(new ClientMessage(Operation.LogOff,new KeyValuePair<string, string>(_username,"OFF")));
        }

        public void SendButton_Click(object sender, EventArgs e)
        {
            _message = messageTextBox.Text;
            UpdateListView(_message);
            if (_InputHandler(new ClientMessage(Operation.Message, new KeyValuePair<string, string>(_username, _message))))
            {
                messageTextBox.Text = "";
            }
        }
        
        public bool ClientMessageReceived(ClientMessage message)
        {
            UpdateListView(message.GetData.Value);
            return true;
        }

        public string GetMessageToSend()
        {
            return _message;
        }


        public void UpdateListView(string s)
        {
            Invoke(new Action(() => messageListBox.TopIndex = messageListBox.Items.Add(s)));
        }
    }
}
