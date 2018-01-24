using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using FinalProject_T10_A_ClassLibrary;

namespace ChatClient
{
   // public delegate bool Message(Operation op);

    public delegate bool Message(ClientMessage cmsg);

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm lf = new LoginForm(); //Starting form
            Application.Run(lf);

            string username = lf.Username;
            string password = lf.Password;

            WebSocket ws = new WebSocket("ws://127.0.0.1:8000/server"); // Open connection
            ws.Close(); //I keep forgeting to close the connection
            ws.Connect();
            
            ClientModelManager model = new ClientModel(username,password);
            ClientController c = new ClientController(ws);
            ChatForm view = new ChatForm(username, c.MessageEntered);
          
            c.Register(model, view);
   
            c.GetCMessage += view.ClientMessageReceived;
            Application.Run(view);
            c.LogOff();
            
            ws.Close();
        }
    }
}
