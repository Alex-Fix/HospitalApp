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
    /// Interaction logic for ViewAdmissionsForm.xaml
    /// </summary>
    public partial class ViewAdmissionsForm : Window
    {
        private readonly TcpService tcpService;

        private ObservableCollection<Admission> admissions = new ObservableCollection<Admission>();

        public static async Task<ViewAdmissionsForm> CreateAsyncViewAdmissionsForm()
        {
            ViewAdmissionsForm form = new ViewAdmissionsForm();
            await form.InitializeAsync();
            return form;
        }

        private ViewAdmissionsForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync()
        {
            try
            {
                InitializeComponent();
                var adm = await InitModelsInForm();
                foreach (var el in adm)
                {
                    admissions.Add(el);
                }
                AdmissionsListBox.ItemsSource = admissions;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<List<Admission>> InitModelsInForm()
        {
            try
            {
                string request = tcpService.SerializeInitAdmissionsInViewAdmissionsForm(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<Admission> models = tcpService.DeseializeInitAdmissionsInViewAdmissionsForm(response);
                return models;
            }
            catch (Exception)
            {
                return new List<Admission>();
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

                object admission = AdmissionsListBox.SelectedItem;
                int? id = ((Admission)admission)?.Id;
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

                string request = tcpService.SerializeDeleteAdmission(id.Value, SingletoneObj.User);
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
                    admissions.Remove((Admission)admission);
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

        private async void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteBtn.IsEnabled = false;
                DetailsBtn.IsEnabled = false;
                ExitBtn.IsEnabled = false;

                object admission = AdmissionsListBox.SelectedItem;
                int? id = ((Admission)admission)?.Id;
                if (!id.HasValue)
                    throw new ArgumentException($"{nameof(id)} is incorrect");

                DetalilsAdmissionForm form = await DetalilsAdmissionForm.CreateAsyncDetailsAdmissionForm(((Admission)admission));
                form.ShowDialog();
                admissions.Remove((Admission)admission);
                var adm = (Admission)admission;
                adm.DischargeDate = SingletoneObj.DischargeDate;
                admissions.Add(adm);
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
