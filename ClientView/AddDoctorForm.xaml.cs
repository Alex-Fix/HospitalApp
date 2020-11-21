using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AddDoctorForm.xaml
    /// </summary>
    public partial class AddDoctorForm : Window
    {
        private readonly TcpService tcpService;
        public AddDoctorForm()
        {
            InitializeComponent();
            DateOfBirthDataPicker.SelectedDate = DateTime.Now;
            tcpService = new TcpService();
        }

        private async void AddDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddDoctorBtn.IsEnabled = false;
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string middleName = MiddleNameTextBox.Text;
                string phone = PhoneTextBox.Text;
                string specialization = SpecializationTextBox.Text;
                DateTime dateOfBirth;
                if (string.IsNullOrWhiteSpace(firstName))
                    throw new ArgumentNullException($"'{nameof(firstName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentNullException($"'{nameof(lastName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(middleName))
                    throw new ArgumentNullException($"'{nameof(middleName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(phone) || phone.Length !=10 || !Regex.IsMatch(phone, "0[0-9]{9}"))
                    throw new ArgumentNullException($"'{nameof(phone)}' is incorrect");
                if (string.IsNullOrWhiteSpace(specialization))
                    throw new ArgumentNullException($"'{nameof(specialization)}' is incorrect");
                if (!DateOfBirthDataPicker.SelectedDate.HasValue || DateOfBirthDataPicker.SelectedDate.Value > DateTime.Now)
                    throw new ArgumentNullException($"'{nameof(dateOfBirth)}' is incorrect");

                dateOfBirth = DateOfBirthDataPicker.SelectedDate.Value;

                Doctor requestDoctor = new Doctor
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Phone = phone,
                    Specialization = specialization,
                    DateOfBirth = dateOfBirth
                };

                string request = tcpService.SerializeAddDoctorRequest(requestDoctor, SingletoneObj.User);
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
                AddDoctorBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
