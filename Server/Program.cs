using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static TcpListener listener;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            using (ApplicationContext context = new ApplicationContext())
            {
                int port;
                string ip;
                do
                {
                    Console.Write("Enter port: ");
                }
                while (!int.TryParse(Console.ReadLine(), out port));
                Console.Write("Enter ip: ");
                ip = Console.ReadLine();
                try
                {
                    listener = new TcpListener(IPAddress.Parse(ip), port);
                    listener.Start();
                    Console.WriteLine("Waiting for connections ...");

                    while (true)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        ClientObject clientObject = new ClientObject(client, context);
                        Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                        clientThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (listener != null)
                    {
                        listener.Stop();
                    }
                }
            }
        }
    }
}
