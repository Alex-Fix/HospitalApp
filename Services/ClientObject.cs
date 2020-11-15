using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientObject
    {
        public TcpClient client;
        private readonly TcpService tcpService;
        public ClientObject(TcpClient tcpClient, ApplicationContext context)
        {
            client = tcpClient;
            tcpService = new TcpService(context);
        }

        public void Process()
        {
            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] data = new byte[64];
                while (true)
                {
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    string request = builder.ToString();

                    Console.WriteLine(request);

                    string response = tcpService.DecodeAndProcessRequest(request);

                    data = Encoding.UTF8.GetBytes(response);

                    stream.Write(data, 0, data.Length);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if(stream != null)
                {
                    stream.Close();
                }
                if(client!= null)
                {
                    client.Close();
                }
            }
        }
    }
}
