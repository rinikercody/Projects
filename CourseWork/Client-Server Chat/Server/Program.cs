using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;
using System.Windows.Forms;

namespace FinalProject_T10_A_Server
{
    public delegate void Display();

    static class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainView mv = new MainView(c.Handle);
            c.Register(mv.UpdateDisplay);
            Application.Run(mv);
            ModelManager m = new Model();
            var wss = new WebSocketServer(8000);
            Controller c = new Controller(m, wss);
            wss.AddWebSocketService("/server", () => c);
            wss.Start();
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
            c.ShutDown();
            wss.Stop();
        }
    }
}