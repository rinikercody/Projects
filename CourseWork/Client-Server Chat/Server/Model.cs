using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace FinalProject_T10_A_Server
{
    class Model : ModelManager
    {
        /// <summary>
        /// Holds user and key value pair of password and contacts
        /// </summary>
        Dictionary<string, KeyValuePair<string, List<string>>> _clients;

        /// <summary>
        /// Creates a new model using a list of clients
        /// </summary>
        public Model()
        {
            using (StreamReader sr = File.OpenText("Clients.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                _clients = (Dictionary<string, KeyValuePair<string, List<string>>>)serializer.Deserialize(sr, typeof(Dictionary<string, KeyValuePair<string, List<string>>>));
            }
        }

        /// <summary>
        /// Writes current clients to a JSON file to safely shut down
        /// </summary>
        public void ShutDown()
        {
            File.WriteAllText("Clients.json", JsonConvert.SerializeObject(_clients));
            using (StreamWriter sw = File.CreateText("Clients.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(sw, _clients);
            }
        }

        /// <summary>
        /// Adds a new contact to a specified user
        /// </summary>
        /// <param name="user">The name of the user to add a contact to</param>
        /// <param name="contact">The new contact to add</param>
        /// <returns>Returns whether adding the contact was successful or not</returns>
        public bool AddContact(string user, string contact)
        {
            if (_clients.ContainsKey(user))
            {
                if (!_clients[user].Value.Contains(contact))
                {
                    _clients[user].Value.Add(contact);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the list of contacts for the specified user
        /// </summary>
        /// <param name="user">The user to look up</param>
        /// <returns>A List of the user's contacts</returns>
        public List<string> GetContacts(string user)
        {
            return _clients[user].Value;
        }

        /// <summary>
        /// Attempts to log in to the server
        /// </summary>
        /// <param name="user">The username to attempt</param>
        /// <param name="password">The password to attempt</param>
        /// <returns>Returns whether the login attempt was successful or not</returns>
        public bool LogIn(string user, string password)
        {
            if (_clients.ContainsKey(user))
            {
                return _clients[user].Key == password;
            }
            else
            {
                _clients.Add(user, new KeyValuePair<string, List<string>>(password, new List<string>()));
                return true;
            }
        }

        /// <summary>
        /// Attempts to remove a contact from the specified user
        /// </summary>
        /// <param name="user">The user whose contact is being removed</param>
        /// <param name="contact">The contact to try to remove</param>
        /// <returns>Returns if removing the contact was successful or not</returns>
        public bool RemoveContact(string user, string contact)
        {
            if (_clients.ContainsKey(user))
            {
                if (_clients[user].Value.Contains(contact))
                {
                    _clients[user].Value.Remove(contact);
                    return true;
                }
            }
            return false;
        }
    }
}