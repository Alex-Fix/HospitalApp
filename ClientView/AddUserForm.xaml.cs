using Data;
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
    /// Interaction logic for AddUserForm.xaml
    /// </summary>
    public partial class AddUserForm : Window
    {
        private readonly TcpService tcpService;
        
        public static async Task<AddUserForm> CreateAsyncAddUserForm()
        {
            AddUserForm form = new AddUserForm();
            await form.InitializeAsync();
            return form;
        }

        private AddUserForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync()
        {
            List<Role> roles = await InitRolesInForm();
            InitializeComponent();
            RoleListBox.ItemsSource = roles;
        }

        private async Task<List<Role>> InitRolesInForm()
        {
            try
            {
                string request = tcpService.SerializeInitRolesInFormRequest(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<Role> roles = tcpService.DeserializeInitRolesInFormResponse(response);
                return roles;
            }
            catch (Exception)
            {
                return new List<Role>();
            }
        }

        private async void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddUserBtn.IsEnabled = false;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;
                string confirmPassword = ConfirmPasswordTextBox.Text;
                List<Role> selectedRoles = new List<Role>();
                foreach(var item in RoleListBox.SelectedItems)
                {
                    selectedRoles.Add((Role)item);
                }
                
                if (string.IsNullOrWhiteSpace(login))
                    throw new ArgumentNullException($"'{nameof(login)}' is incorrect");
                if (string.IsNullOrWhiteSpace(password) || !password.Equals(confirmPassword))
                    throw new ArgumentNullException($"'{nameof(password)}' is incorrect");
                if (selectedRoles.Count() == 0)
                    throw new ArgumentNullException($"'{nameof(selectedRoles)}' is incorrect");

                var requestUser = new User
                {
                    Login = login,
                    Password = password,
                    Role_User_Mappings = selectedRoles.Select(x => new Role_User_Mapping
                    {
                        Role = new Role
                        {
                            RoleName = x.RoleName,
                            Id = x.Id
                        }
                    }).ToList()
                };

                string request = tcpService.SerializeAddUserRequest(requestUser, SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                var responseArgs = response.Split(';');
                if (responseArgs.Length > 1 && responseArgs[0].Contains("500"))
                {
                    throw new ArgumentException(responseArgs[1]);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                AddUserBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
