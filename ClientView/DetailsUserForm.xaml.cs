using Data;
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
    /// Interaction logic for DetailsUserForm.xaml
    /// </summary>
    public partial class DetailsUserForm : Window
    {
        public DetailsUserForm(User user)
        {
            InitializeComponent();
            LoginLabel.Content += user.Login;
            PasswordLabel.Content += user.Password;
            var roles = user.Role_User_Mappings.Select(el => el.Role.RoleName).ToList();
            var str = "";
            for(int i = 1; i< roles.Count()+1; i++)
            {
                if(i%3 == 0)
                {
                    str += roles[i-1]+",\n";
                    continue;
                }
                str += roles[i-1] + ", ";
            }
            RoleLabel.Content += str.Remove(str.Length - 2);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
