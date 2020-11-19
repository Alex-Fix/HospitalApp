using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Data;
using System.Reflection;

namespace Services
{
    public class TcpService
    {
        private readonly DataService dataService;
        public TcpService(ApplicationContext context= null)
        {
            if(context != null)
                dataService = new DataService(context);
        }


        public string DecodeAndProcessRequest(string request)
        {
            var socketNode = JsonSerializer.Deserialize<SocketNode>(request);
            TcpMethods tcpMethod; 
            string response = "";
            if(!Enum.TryParse<TcpMethods>(socketNode.Method, out tcpMethod))
            {
                tcpMethod = TcpMethods.NONE;
            }
                    

            switch (tcpMethod)
            {
                case TcpMethods.Authorize:
                    response = Authorize(socketNode);
                    break;
                case TcpMethods.AddPatient:
                    response = AddPatient(socketNode);
                    break;
                case TcpMethods.AddDoctor:
                    response = AddDoctor(socketNode);
                    break;
                default:
                    break;
            }

            return response; 
        }

        public async Task<string> DecodeStreamAsync(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = await stream.ReadAsync(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        public string DecodeStream(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        public async Task<byte[]> CodeStreamAsync(string request)
        {
            return await Task.Run(() => CodeStream(request));
        }

        public byte[] CodeStream(string request)
        {
            return Encoding.UTF8.GetBytes(request);
        }

        public string SerializeAuthorizeRequest(string login, string password)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "Authorize",
                User = JsonSerializer.Serialize<User>(new User
                {
                    Login = login,
                    Password = password
                })
            });
        }

        public User DeserializeAuthorizeResponse(string response)
        {
            return JsonSerializer.Deserialize<User>(response);
        }

        private string Authorize(SocketNode node)
        {
            string response = "";
            User requestUser = JsonSerializer.Deserialize<User>(node.User);

            if(requestUser!= null)
            {
                User user = dataService.GetUser(requestUser.Login, requestUser.Password);
                if(user == null)
                {
                    user = new User();
                }
                else
                {
                    foreach(var el in user.Role_User_Mappings)
                    {
                        el.User = null;
                        var mapings = el.Role.Role_User_Mappings;
                        el.Role.Role_User_Mappings = null;
                    }
                }
                response = JsonSerializer.Serialize<User>(user);
            }

            return response;
        }

        public string SerializeAddPatientRequest(Patient patient)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddPatient",
                User = "",
                Args = JsonSerializer.Serialize<Patient>(patient)
            });
        }

        private string AddPatient(SocketNode socketNode)
        {
            try
            {
                var requestPatient = JsonSerializer.Deserialize<Patient>(socketNode.Args);
                dataService.InsertPatient(requestPatient);
                return "200";
            }
            catch(Exception ex)
            {
                return "500;" + ex.Message;
            }
        }


        public string SerializeAddDoctorRequest(Doctor doctor)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddDoctor",
                User = "",
                Args = JsonSerializer.Serialize<Doctor>(doctor)
            });
        }

        private string AddDoctor(SocketNode socketNode)
        {
            try
            {
                var requestDoctor = JsonSerializer.Deserialize<Doctor>(socketNode.Args);
                dataService.InsertDoctor(requestDoctor);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

    }

    

    public enum TcpMethods
    {
        NONE,
        Authorize,
        AddPatient,
        AddDoctor
    }
}
