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
        }

        private void AddPatientBtn_Click(object sender, EventArgs e)
        {
            AddPatientForm form = new AddPatientForm();
            form.ShowDialog();
        }
    }
}
