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

        private void AddDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            AddDoctorForm form = new AddDoctorForm();
            form.ShowDialog();
        }

        private async void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUserForm form = await AddUserForm.CreateAsyncAddUserForm();
            form.ShowDialog();
        }

        private void AddWardBtn_Click(object sender, RoutedEventArgs e)
        {
            AddWardForm form = new AddWardForm();
            form.ShowDialog();
        }

        private void AddMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            AddMedicineForm form = new AddMedicineForm();
            form.ShowDialog();
        }

        private async void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddPatientBtn.IsEnabled = false;
                AddDoctorBtn.IsEnabled = false;
                AddUserBtn.IsEnabled = false;
                ViewPatientsBtn.IsEnabled = false;
                ViewDoctorsBtn.IsEnabled = false;
                ViewUsersBtn.IsEnabled = false;
                AddWardBtn.IsEnabled = false;
                AddMedicineBtn.IsEnabled = false;
                AddAddmitionBtn.IsEnabled = false;
                ViewWardBtn.IsEnabled = false;
                ViewMedicineBtn.IsEnabled = false;
                ViewAddmitionBtn.IsEnabled = false;
                ExitBtn.IsEnabled = false;

                string request = "LogOut";
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);

            }
            catch (Exception) { }
            finally
            {
                if (SingletoneObj.Stream != null)
                    SingletoneObj.Stream.Close();
                if (SingletoneObj.Client != null)
                    SingletoneObj.Client.Close();
                SingletoneObj.Windows.Add("MainForm", this);
                SingletoneObj.User = null;
                AuthorizationForm form = new AuthorizationForm();
                form.Show();
            }
        }

        private async void AddAddmitionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddAdmissionForm form = await AddAdmissionForm.CreateAsyncAddAdmissionForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }

        private async void ViewPatientsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewPatientsForm form = await ViewPatientsForm.CreateAsyncViewPatientsForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }

        private async void ViewDoctorsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewDoctorsForm form = await ViewDoctorsForm.CreateAsyncViewDoctorsForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }

        private async void ViewUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewUsersForm form = await ViewUsersForm.CreateAsyncViewUsersForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }

        private async void ViewMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewMedicinesForm form = await ViewMedicinesForm.CreateAsyncViewMedicinesForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }

        private async void ViewAddmitionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewAdmissionsForm form = await ViewAdmissionsForm.CreateAsyncViewAdmissionsForm();
                form.ShowDialog();
            }
            catch (Exception) { }
        }
    }
}
