using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_T10_A_Server
{
    interface ModelManager
    {
        bool AddContact(string user, string contact);
        bool LogIn(string user, string password);
        bool RemoveContact(string user, string contact);
        void ShutDown();
        List<string> GetContacts(string user);
    }
}