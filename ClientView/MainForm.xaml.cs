using Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private readonly TcpService tcpService;

        public MainForm()
        {
            InitializeComponent();
            if (SingletoneObj.Windows.ContainsKey("AuthorizationForm"))
            {
                SingletoneObj.Windows["AuthorizationForm"].Close();
                SingletoneObj.Windows.Remove("AuthorizationForm");
            }
            bool IsAdmin = SingletoneObj.User.Role_User_Mappings.Select(el => el.Role.RoleName).Contains("Admin");
            this.AddUserBtn.IsEnabled = IsAdmin;
            this.ViewUsersBtn.IsEnabled = IsAdmin;
            this.IPLabel.Content = "IP: " + SingletoneObj.IP;
            this.PortLabel.Content = "Port: " + SingletoneObj.Port;
            this.LoginLabel.Content = "Login: " + SingletoneObj.User.Login;
            tcpService = new TcpService();
        }

        private void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddPatientForm form = new AddPatientForm();
            form.ShowDialog();
        }
    }
}
