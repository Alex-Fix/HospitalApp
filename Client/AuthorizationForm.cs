using Data;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AuthorizationForm : Form
    {
        private readonly TcpService tcpService;

        public AuthorizationForm()
        {
            InitializeComponent();
            this.tcpService = new TcpService();
            Program.forms.Add(this);
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(this.PortTextBox.Text);
                if (port < 0)
                    throw new ArgumentException("'port' is incorrect");
                if (string.IsNullOrWhiteSpace(this.IPTextBox.Text)) 
                    throw new ArgumentException("'ip' is incorrect");
                if (string.IsNullOrWhiteSpace(this.LoginTextBox.Text)) 
                    throw new ArgumentException("'login' is incorrect");
                if (string.IsNullOrWhiteSpace(this.PasswordTextBox.Text))
                    throw new ArgumentException("'pasword' is incorrect");
                string address = this.IPTextBox.Text;
                string login = this.LoginTextBox.Text;
                string password = this.PasswordTextBox.Text;

                Program.client = new TcpClient(address, port);
                Program.stream = Program.client.GetStream();


                string request = tcpService.SerializeAuthorizeRequest(login, password);
                byte[] data = tcpService.CodeStream(request);
                Program.stream.Write(data, 0, data.Length);
                string response = tcpService.DecodeStream(Program.stream);
                User user = tcpService.DeserializeAuthorizeResponse(response);
                if (user.Login == null || user.Password == null || !user.Login.Equals(login) || !user.Password.Equals(password))
                    throw new ArgumentException("login or password is incorrect");
                Program.user = user;
                Program.ip = address;
                Program.port = port;
                Form mainForm = new MainForm();
                mainForm.Left = this.Left;
                mainForm.Top = this.Top;
                mainForm.Show();
                this.Hide();
                
            }
            catch(Exception ex){
                this.StatusLabel.Text = "Status: " + ex.Message;
                if (Program.client != null)
                    Program.client.Close();
                if (Program.stream != null)
                    Program.stream.Close();
            }
        }
    }
}
