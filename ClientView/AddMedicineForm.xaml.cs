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
    /// Interaction logic for AddMedicineForm.xaml
    /// </summary>
    public partial class AddMedicineForm : Window
    {
        private readonly TcpService tcpService;
        
        public AddMedicineForm()
        {
            InitializeComponent();
            tcpService = new TcpService();
        }

        private async void AddDoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddMedicineBtn.IsEnabled = false;
                string name = NameTextBox.Text;
                string sku = SkuTextBox.Text;
                decimal price;
                string indication = IndicationTextBox.Text;
                string country = CountryTextBox.Text;

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentNullException($"'{nameof(name)}' is incorrect");
                if (string.IsNullOrWhiteSpace(sku))
                    throw new ArgumentNullException($"'{nameof(sku)}' is incorrect");
                if (string.IsNullOrWhiteSpace(indication))
                    throw new ArgumentNullException($"'{nameof(indication)}' is incorrect");
                if (string.IsNullOrWhiteSpace(country))
                    throw new ArgumentNullException($"'{nameof(country)}' is incorrect");
                if (!decimal.TryParse(PriceTextBox.Text, out price))
                    throw new ArgumentNullException($"'{nameof(price)}' is incorrect");
                if (price < 0)
                    throw new ArgumentNullException($"'{nameof(price)}' is incorrect");

                Medicine requestMedicine = new Medicine
                {
                    Name = name,
                    Price = price,
                    Sku = sku,
                    Indication = indication,
                    Country = new Country
                    {
                        Name = country
                    }
                };

                string request = tcpService.SerializeAddMedicineRequest(requestMedicine, SingletoneObj.User);
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
                AddMedicineBtn.IsEnabled = true;
                StatusLabel.Content = "Status: " + ex.Message;
            }
        }
    }
}
