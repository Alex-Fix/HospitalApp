using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        public static TcpClient client;
        public static NetworkStream stream;
        public static User user;
        public static List<Form> forms;
        public static string ip;
        public static int port;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            forms = new List<Form>();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //  Application.Run(new AuthorizationForm());
            Application.Run(new AuthorizationForm());
        }
    }
}
