using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class MainForm : Form
    {
        private readonly TcpService tcpService;
        public MainForm()
        {
            InitializeComponent();
            Program.forms.Add(this);
            tcpService = new TcpService();
            this.AddUserBtn.Enabled = Program.user.Role_User_Mappings.Any(el => el.Role.RoleName.Equals("Admin"));
            this.ViewUsersBtn.Enabled = Program.user.Role_User_Mappings.Any(el => el.Role.RoleName.Equals("Admin"));
            this.PortLabel.Text = "Port: " + Program.port.ToString();
            this.IPLabel.Text = "IP: " + Program.ip;
            this.LoginLabel.Text = "Login: " + Program.user.Login;
        }

        private void AddPatientBtn_Click(object sender, EventArgs e)
        {
            AddPatientForm form = new AddPatientForm();
            form.ShowDialog();
        }

        private void AddDoctorBtn_Click(object sender, EventArgs e)
        {
            AddDoctorForm form = new AddDoctorForm();
            form.ShowDialog();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
