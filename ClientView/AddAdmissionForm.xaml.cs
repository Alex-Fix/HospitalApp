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
    /// Interaction logic for AddAdmissionForm.xaml
    /// </summary>
    public partial class AddAdmissionForm : Window
    {
        private readonly TcpService tcpService;

        public static async Task<AddAdmissionForm> CreateAsyncAddAdmissionForm()
        {
            AddAdmissionForm form = new AddAdmissionForm();
            await form.InitializeAsync();
            return form;
        }

        private AddAdmissionForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync()
        {
            try
            {
                List<string> initialModels = await InitModelsInForm();
                if (initialModels.Count() != 3)
                    throw new Exception();
                List<Patient> patients =  tcpService.InitPatientsInForm(initialModels[0]);
                List<Ward> wards = tcpService.InitWardsInForm(initialModels[1]);
                List<Doctor> doctors = tcpService.InitDoctorsInForm(initialModels[2]);
                InitializeComponent();
                PatientListBox.ItemsSource = patients;
                WardListBox.ItemsSource = wards;
                DoctorListBox.ItemsSource = doctors;
                DateOfReceiptPicker.SelectedDate = DateTime.Now;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        private async Task<List<string>> InitModelsInForm()
        {
            try
            {
                string request = tcpService.SerializeInitModelsInAddAdmissionForm(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<string> models = tcpService.DeseializeInitModelsInAddAdmissionForm(response);
                return models;
            }
            catch(Exception)
            {
                return new List<string>();
            }
        }

        private async void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddAdmissionBtn.IsEnabled = false;
                int? patientId = ((Patient)PatientListBox.SelectedItem)?.Id;
                int? wardId = ((Ward)WardListBox.SelectedItem)?.Id;
                int? doctorId = ((Doctor)DoctorListBox.SelectedItem)?.Id;
                DateTime? dateOfReceipt = DateOfReceiptPicker.SelectedDate;
                string diagnosis = DiagnosisTextBox.Text;

                if (!patientId.HasValue)
                    throw new ArgumentNullException($"'{nameof(patientId)}' is incorrect");
                if (!wardId.HasValue)
                    throw new ArgumentNullException($"'{nameof(wardId)}' is incorrect");
                if (!doctorId.HasValue)
                    throw new ArgumentNullException($"'{nameof(doctorId)}' is incorrect");
                if (!dateOfReceipt.HasValue || dateOfReceipt.Value > DateTime.Now)
                    throw new ArgumentNullException($"'{nameof(dateOfReceipt)}' is incorrect");
                if (string.IsNullOrWhiteSpace(diagnosis))
                    throw new ArgumentNullException($"'{nameof(diagnosis)}' is incorrect");

                Admission requestAdmission = new Admission
                {
                    PatientId = patientId.Value,
                    WardId = wardId.Value,
                    DoctorId = doctorId.Value,
                    DateOfReceipt = dateOfReceipt.Value,
                    Diagnosis = diagnosis
                };

                string request = tcpService.SerializeAddAdmissionRequest(requestAdmission, SingletoneObj.User);
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
                AddAdmissionBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
