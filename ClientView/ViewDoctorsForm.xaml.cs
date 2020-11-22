using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ViewDoctorsForm.xaml
    /// </summary>
    public partial class ViewDoctorsForm : Window
    {
        private readonly TcpService tcpService;

        private ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();

        public static async Task<ViewDoctorsForm> CreateAsyncViewDoctorsForm()
        {
            ViewDoctorsForm form = new ViewDoctorsForm();
            await form.InitializeAsync();
            return form;
        }

        private ViewDoctorsForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync()
        {
            try
            {
                InitializeComponent();
                var doc = await InitModelsInForm();
                foreach (var el in doc)
                {
                    doctors.Add(el);
                }
                DoctorsListBox.ItemsSource = doctors;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<List<Doctor>> InitModelsInForm()
        {
            try
            {
                string request = tcpService.SerializeInitDoctorsInViewDoctorsForm(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<Doctor> models = tcpService.DeseializeInitDoctorsInViewDoctorsForm(response);
                return models;
            }
            catch (Exception)
            {
                return new List<Doctor>();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteBtn.IsEnabled = false;
                DetailsBtn.IsEnabled = false;
                ExitBtn.IsEnabled = false;

                object doctor = DoctorsListBox.SelectedItem;
                int? id = ((Doctor)doctor)?.Id;
                if (!id.HasValue)
                    throw new ArgumentException($"{nameof(id)} is incorrect");

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    DeleteBtn.IsEnabled = true;
                    DetailsBtn.IsEnabled = true;
                    ExitBtn.IsEnabled = true;
                    return;
                }

                string request = tcpService.SerializeDeleteDoctor(id.Value, SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);

                var responseArgs = response.Split(';');
                if (responseArgs.Length > 1 && responseArgs[0].Contains("500"))
                {
                    throw new ArgumentException(responseArgs[1]);
                }
                if (responseArgs.Length == 1 && responseArgs[0].Equals("200"))
                {
                    doctors.Remove((Doctor)doctor);
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Content = "Status: " + ex.Message;
            }
            finally
            {
                DeleteBtn.IsEnabled = true;
                DetailsBtn.IsEnabled = true;
                ExitBtn.IsEnabled = true;
            }
        }

        private void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteBtn.IsEnabled = false;
                DetailsBtn.IsEnabled = false;
                ExitBtn.IsEnabled = false;

                object doctor = DoctorsListBox.SelectedItem;
                int? id = ((Doctor)doctor)?.Id;
                if (!id.HasValue)
                    throw new ArgumentException($"{nameof(id)} is incorrect");

                DetailsDoctorForm form = new DetailsDoctorForm((Doctor)doctor);
                form.ShowDialog();

            }
            catch (Exception ex)
            {
                StatusLabel.Content = "Status: " + ex.Message;
            }
            finally
            {
                DeleteBtn.IsEnabled = true;
                DetailsBtn.IsEnabled = true;
                ExitBtn.IsEnabled = true;
            }
        }
    }
}
