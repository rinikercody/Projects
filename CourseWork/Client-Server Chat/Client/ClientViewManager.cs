using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject_T10_A_ClassLibrary;

namespace ChatClient
{
    interface ClientViewManager
    {
        void ViewContactsButton(object sender, EventArgs e);
        void SendButton_Click(object sender, EventArgs e);
        bool MessageReceived(Operation op);
        string GetMessageToSend();
        void UpdateListView(string s);
        bool ClientMessageReceived(ClientMessage cmsg);
    }
}
