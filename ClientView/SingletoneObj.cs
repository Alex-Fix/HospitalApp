using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientView
{
    public static class SingletoneObj
    {
        public static User User { get; set; }
        public static int Port { get; set; }
        public static string IP { get; set; }
        public static TcpClient Client { get; set; }
        public static NetworkStream Stream { get; set; }
        public static Dictionary<string,Window> Windows { get; set; } = new Dictionary<string, Window>();
    }
}
