using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using FinalProject_T10_A_ClassLibrary;

namespace ChatClient
{
    class ClientController
    {
        private WebSocket _ws; //The websocket
        private ClientModelManager _model; // Interface of the model
        private ClientViewManager _view; // Interface of the chatform

        public event Message GetCMessage;

        public ClientController(WebSocket ws)
        {
           _ws = ws;
            //ws.OnMessage += (sender, e) => { if (MessageReceived != null) MessageEntered(Operation.Message); };
            _ws.OnMessage += (sender, e) => _view.UpdateListView(e.Data);
            _ws.Connect();
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Registers the model and the view
        /// </summary>
        /// <param name="model">The main model for the program</param>
        /// <param name="chatform">The chat form that will be used</param>
        public void Register(ClientModelManager model, ClientViewManager chatform)
        {
            _model = model;
            _view = chatform;
            MessageEntered(new ClientMessage(Operation.LogIn, _model.GetData()));
        }

        /// <summary>
        /// Sends ClientMessage to model as a json string based off operation
        /// </summary>
        /// <param name="csmg">Client message contianing data to be used</param>
        /// <returns>Rturns true if the websocket is still active.</returns>
        //public bool MessageEntered(Operation op) // public bool MessageEntered(ClientMessage cmsg)
        public bool MessageEntered(ClientMessage cmsg)
        {
            if (_ws.IsAlive)
            {
                switch (cmsg.GetOperation)
                {
                    case Operation.Message:
                        _ws.Send(_model.SendMessage(cmsg.GetData.Value));
                        break;
                    case Operation.LogIn:
                        _ws.Send(_model.LogIn());
                        break;
                    case Operation.LogOff:
                        _ws.Send(_model.LogOut());
                        break;
                    case Operation.AddContact:
                        _ws.Send(_model.AddContact(cmsg.GetData.Value));
                        break;
                    case Operation.RemoveContact:
                        _ws.Send(_model.RemoveContact(cmsg.GetData.Value));
                        break;
                    case Operation.CreateChat:
                        _ws.Send(_model.CreateChat(cmsg.GetData.Value));
                        break;
                    case Operation.LeaveChat:
                        _ws.Send(_model.LeaveChat(cmsg.GetData.Value));
                        break;
                    default:
                        break;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void LogOff()
        {
            _ws.Send(_model.LogOut());
        }
    }
}
