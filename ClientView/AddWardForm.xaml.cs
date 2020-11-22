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
    /// Interaction logic for AddWardForm.xaml
    /// </summary>
    public partial class AddWardForm : Window
    {
        private readonly TcpService tcpService;
        public AddWardForm()
        {
            InitializeComponent(); 
            ComfortListBox.ItemsSource = Enum.GetNames(typeof(Comfort)).ToList();
            tcpService = new TcpService();
        }

        private async void AddWardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddWardBtn.IsEnabled = false;
                int wardNumber;
                Comfort comfort;
                int numberOfPlaces;

                if (!int.TryParse(WardNumberTextBox.Text, out wardNumber))
                    throw new ArgumentNullException($"'{nameof(wardNumber)}' is incorrect");
                if (!int.TryParse(CountOfPlacesTextBox.Text, out numberOfPlaces))
                    throw new ArgumentNullException($"'{nameof(numberOfPlaces)}' is incorrect");
                if (!Enum.TryParse(ComfortListBox.SelectedItem.ToString(), out comfort))
                   throw new ArgumentNullException($"'{nameof(comfort)}' is incorrect");

                Ward requestWard = new Ward
                {
                    WardNumber = wardNumber,
                    NumberOfPaces = numberOfPlaces,
                    Comfort = comfort
                };

                string request = tcpService.SerializeAddWardRequest(requestWard, SingletoneObj.User);
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
                AddWardBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
