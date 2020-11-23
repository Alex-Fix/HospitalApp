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
    /// Interaction logic for ViewWardsForm.xaml
    /// </summary>
    public partial class ViewWardsForm : Window
    {
        private readonly TcpService tcpService;

        private ObservableCollection<Ward> wards = new ObservableCollection<Ward>();

        public static async Task<ViewWardsForm> CreateAsyncViewWardsForm()
        {
            ViewWardsForm form = new ViewWardsForm();
            await form.InitializeAsync();
            return form;
        }

        private ViewWardsForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync()
        {
            try
            {
                InitializeComponent();
                var war = await InitModelsInForm();
                foreach (var el in war)
                {
                    wards.Add(el);
                }
                WardsListBox.ItemsSource = wards;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<List<Ward>> InitModelsInForm()
        {
            try
            {
                string request = tcpService.SerializeInitWardsInViewWardsForm(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<Ward> models = tcpService.DeseializeInitWardsInViewWardsForm(response);
                return models;
            }
            catch (Exception)
            {
                return new List<Ward>();
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
                ExitBtn.IsEnabled = false;

                object ward = WardsListBox.SelectedItem;
                int? id = ((Ward)ward)?.Id;
                if (!id.HasValue)
                    throw new ArgumentException($"{nameof(id)} is incorrect");

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    DeleteBtn.IsEnabled = true;
                    ExitBtn.IsEnabled = true;
                    return;
                }

                string request = tcpService.SerializeDeleteWard(id.Value, SingletoneObj.User);
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
                    wards.Remove((Ward)ward);
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Content = "Status: " + ex.Message;
            }
            finally
            {
                DeleteBtn.IsEnabled = true;
                ExitBtn.IsEnabled = true;
            }
        }
    }
}
