using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientView
{
    /// <summary>
    /// Interaction logic for AuthorizationForm.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        private readonly TcpService tcpService;
        public AuthorizationForm()
        {
            InitializeComponent();
            if (SingletoneObj.Windows.ContainsKey("MainForm"))
            {
                SingletoneObj.Windows["MainForm"].Close();
                SingletoneObj.Windows.Remove("MainForm");
            }
            this.tcpService = new TcpService();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginBtn.IsEnabled = false;
                int port = int.Parse(this.PortTextBox.Text);
                if (port < 0)
                    throw new ArgumentException("'port' is incorrect");
                if (string.IsNullOrWhiteSpace(this.IPTextBox.Text))
                    throw new ArgumentException("'ip' is incorrect");
                if (string.IsNullOrWhiteSpace(this.LoginTextBox.Text))
                    throw new ArgumentException("'login' is incorrect");
                if (string.IsNullOrWhiteSpace(this.PasswordBox.Password))
                    throw new ArgumentException("'pasword' is incorrect");
                string address = this.IPTextBox.Text;
                string login = this.LoginTextBox.Text;
                string password = this.PasswordBox.Password;

                SingletoneObj.Client = new TcpClient(address, port);
                SingletoneObj.Stream = SingletoneObj.Client.GetStream();


                string request = tcpService.SerializeAuthorizeRequest(login, password);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                User user = tcpService.DeserializeAuthorizeResponse(response);
                if (user.Login == null || user.Password == null || !user.Login.Equals(login) || !user.Password.Equals(password))
                    throw new ArgumentException("login or password is incorrect");
                SingletoneObj.User = user;
                SingletoneObj.IP = address;
                SingletoneObj.Port = port;
                SingletoneObj.Windows.Add("AuthorizationForm", this);
                MainForm form = new MainForm();
                form.Left = (this.Width - form.Width) + this.Left;
                form.Top = (this.Height - form.Height) + this.Top;
                form.Show();
            }
            catch (Exception ex)
            {
                LoginBtn.IsEnabled = true;
                this.StatusLabel.Content = "Status: " + ex.Message;
                if (SingletoneObj.Client != null)
                    SingletoneObj.Client.Close();
                if (SingletoneObj.Stream != null)
                    SingletoneObj.Stream.Close();
            }
        }
    }
}
