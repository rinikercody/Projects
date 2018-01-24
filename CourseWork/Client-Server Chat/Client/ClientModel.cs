using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject_T10_A_ClassLibrary;
using Newtonsoft.Json;

namespace ChatClient
{
    class ClientModel : ClientModelManager
    {
        /// <summary>
        /// Stores the users name and password.
        /// </summary>
        public KeyValuePair<string, string> _data = new KeyValuePair<string, string>();

        /// <summary>
        /// Keeps track of the users contacts
        /// </summary>
        private List<string> _contacts;

        /// <summary>
        /// Inititlizes the user information and creates a new contact list.
        /// </summary>
        /// <param name="username">Clients name</param>
        /// <param name="password">Clients password</param>
        public ClientModel(string username,string password)
        {
            _data = new KeyValuePair<string, string>(username, password);
            _contacts = new List<string>();
        }
        
        //Need to figure out how to the send the text.
        /// <summary>
        /// Used to send text to the server.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        public string SendMessage(string msg)
        {
            ClientMessage cmsg = new ClientMessage(Operation.Message, new KeyValuePair<string,string>(_data.Key,msg));
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// Sends a log in message to the server.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        public string LogIn()
        {
            ClientMessage cmsg =new ClientMessage(Operation.LogIn, _data); // want to send username and password so data works
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// sends a logout message to the server.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        public string LogOut()
        {
            ClientMessage cmsg = new ClientMessage(Operation.LogOff, _data); // want to log off user so data works
            return SerilizeMessage(cmsg);
        }

        //Need to send contact to add
        /// <summary>
        /// Sends an add contact message to the server.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        public string AddContact(string contact)
        {
            ClientMessage cmsg =new ClientMessage(Operation.AddContact, new KeyValuePair<string,string>(_data.Key, contact));
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// Used to send text to the server.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        public string RemoveContact(string contact)
        {
            ClientMessage cmsg = new ClientMessage(Operation.RemoveContact, new KeyValuePair<string, string>(_data.Key, contact));
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// Used to create a chat room
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>String with message to be sent to server</returns>
        public string CreateChat(string contact)
        {
            ClientMessage cmsg = new ClientMessage(Operation.CreateChat, new KeyValuePair<string, string>(_data.Key, contact));
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// Users leaves a chat room
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public string LeaveChat(string contact)
        {
            ClientMessage cmsg = new ClientMessage(Operation.LeaveChat, new KeyValuePair<string, string>(_data.Key, contact));
            return SerilizeMessage(cmsg);
        }

        /// <summary>
        /// Converts a Client message object to a serialized json string.
        /// </summary>
        /// <returns>A json string of the Client Message</returns>
        private string SerilizeMessage(ClientMessage cmsg)
        {
            string json = JsonConvert.SerializeObject(cmsg);

            Operation op = JsonConvert.DeserializeObject<ClientMessage>(json)._operation;
            KeyValuePair<string,string> data = JsonConvert.DeserializeObject<ClientMessage>(json)._data;
            ClientMessage cm = new ClientMessage(op, data);
            
            return json;
        }
     
        /// <summary>
        /// Used to get all the contacts associated with the user.
        /// </summary>
        /// <returns>The users contacts.</returns>
        public List<string> GetContacts()
        {
            return _contacts;
        }

        /// <summary>
        /// Gets data so that it can be displayed by view
        /// </summary>
        /// <returns>The user and the information to be used</returns>
        public KeyValuePair<string,string> GetData()
        {
            return _data;
        }
    }
}
