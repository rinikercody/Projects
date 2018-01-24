using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    interface ClientModelManager
    {
        string LogIn();
        string LogOut();
        string AddContact(string contact);
        string RemoveContact(string contact);
        string SendMessage(string msg);
        string CreateChat(string contact);
        string LeaveChat(string contact);
        List<string> GetContacts();
        KeyValuePair<string,string> GetData();
    }
}
