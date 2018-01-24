using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Newtonsoft.Json;
using FinalProject_T10_A_ClassLibrary;

namespace FinalProject_T10_A_Server
{
    class Controller : WebSocketBehavior
    {
        ModelManager _model;
        WebSocketServer _server;
        List<User> _online2;
        List<string> _online;
        List<string> _chat;
        ServerView sv;

        /// <summary>
        /// Creates a new Controller object given a Model and a WebSocket
        /// </summary>
        /// <param name="m">The Model for the Controller to use</param>
        /// <param name="w">The Web Socket server for the controller to use</param>
        public Controller(ModelManager m, WebSocketServer w)
        {
            _model = m;
            _online = new List<string>();
            _online2 = new List<User>();
            _chat = new List<string>();
            sv = new ServerView();
            sv.Show();
        }

        /// <summary>
        /// Closes the model manager to end the program
        /// </summary>
        public void ShutDown()
        {
            _model.ShutDown();
        }

        /// <summary>
        /// Sends a message from the server to clients
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="b"></param>
        private void SendMessage(ServerMessage msg, bool b)
        {
            if (b)
            {
                Send(JsonConvert.SerializeObject(msg, Formatting.Indented));
            }
            else
            {
                Sessions.Broadcast(JsonConvert.SerializeObject(msg, Formatting.Indented));
            }
        }

        /// <summary>
        /// Defines what the server does when it receives a message from a client
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMessage(MessageEventArgs e)
        {
            lock (this)
            {
                ClientMessage msg = JsonConvert.DeserializeObject<ClientMessage>(e.Data);
                string key = msg.GetData.Key;
                string value = msg.GetData.Value;
                Dictionary<string, bool> d = new Dictionary<string, bool>();
                switch (msg.GetOperation)
                {
                    case Operation.LogIn:
                        if (!_online.Contains(key) && _model.LogIn(key, value))
                        {
                            _online.Add(key);
                            foreach (string s in _model.GetContacts(key))
                            {
                                d.Add(s, _online.Contains(s));
                            }
                            SendMessage(new ServerMessage(Operation.Success, d), true);
                            SendMessage(new ServerMessage(Operation.Online, d), false);
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                    case Operation.LogOff:
                        if (_online.Contains(key))
                        {
                            _online.Remove(key);
                            SendMessage(new ServerMessage(Operation.Success, d), true);
                            SendMessage(new ServerMessage(Operation.Offline, d), false);
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                    case Operation.AddContact:
                        if (_model.AddContact(key, value))
                        {
                            d.Add(value, _online.Contains(value));
                            SendMessage(new ServerMessage(Operation.Success, d), true);
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                    case Operation.RemoveContact:
                        if (_model.RemoveContact(key, value))
                        {

                            SendMessage(new ServerMessage(Operation.Success, d), true);
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                    case Operation.CreateChat:
                        if (_online.Contains(key) && _online.Contains(value))
                        {
                            if (_chat.Count > 0 && _chat.Contains(key) && !_chat.Contains(value))
                            {
                                _chat.Add(value);
                                d.Add(value, true);
                                SendMessage(new ServerMessage(Operation.CreateChat, d), false);
                            }
                            else if (_chat.Count == 0)
                            {
                                _server.AddWebSocketService<Chat>("/chat");
                                _chat.Add(key);
                                _chat.Add(value);
                                d.Add(key, true);
                                d.Add(value, true);
                                SendMessage(new ServerMessage(Operation.CreateChat, d), false);
                            }
                            else
                            {
                                SendMessage(new ServerMessage(Operation.Failure, d), true);
                            }
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                    case Operation.LeaveChat:
                        if (_online.Contains(key) && _chat.Contains(key))
                        {
                            _chat.Remove(key);
                            d.Add(key, true);
                            if (_chat.Count == 0)
                            {
                                _server.RemoveWebSocketService("/chat");
                            }
                            SendMessage(new ServerMessage(Operation.LeaveChat, d), false);
                        }
                        else
                        {
                            SendMessage(new ServerMessage(Operation.Failure, d), true);
                        }
                        break;
                }
            }
        }
    }
}