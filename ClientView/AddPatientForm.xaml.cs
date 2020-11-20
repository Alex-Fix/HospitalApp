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
    /// Interaction logic for AddPatientForm.xaml
    /// </summary>
    public partial class AddPatientForm : Window
    {
        private readonly TcpService tcpService;

        public AddPatientForm()
        {
            InitializeComponent();
            DateOfBirthDataPicker.SelectedDate = DateTime.Now;
            tcpService = new TcpService();
        }

        private async void AddPatientBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddPatientBtn.IsEnabled = false;
                string firstName = FirstNameTextBox.Text;
                string lastName = LastNameTextBox.Text;
                string middleName = MiddleNameTextBox.Text;
                string address = AddressTextBox.Text;
                string insurancePolicy = InsurancePolicyTextBox.Text;
                DateTime dateOfBirth;
                if (string.IsNullOrWhiteSpace(firstName))
                    throw new ArgumentNullException($"'{nameof(firstName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentNullException($"'{nameof(lastName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(middleName))
                    throw new ArgumentNullException($"'{nameof(middleName)}' is incorrect");
                if (string.IsNullOrWhiteSpace(address))
                    throw new ArgumentNullException($"'{nameof(address)}' is incorrect");
                if (string.IsNullOrWhiteSpace(insurancePolicy))
                    throw new ArgumentNullException($"'{nameof(insurancePolicy)}' is incorrect");
                if(!DateOfBirthDataPicker.SelectedDate.HasValue)
                    throw new ArgumentNullException($"'{nameof(dateOfBirth)}' is incorrect");

                dateOfBirth = DateOfBirthDataPicker.SelectedDate.Value;

                Patient requestPatient = new Patient
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Address = address,
                    InsurancePolicy = insurancePolicy,
                    DateOfBirth = dateOfBirth
                };

                string request = tcpService.SerializeAddPatientRequest(requestPatient, SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                var responseArgs = response.Split(';');
                if(responseArgs.Length > 1 && responseArgs[0].Contains("500"))
                {
                    throw new ArgumentException(responseArgs[1]);
                }
                this.Close();
            }
            catch(Exception ex)
            {
                AddPatientBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
