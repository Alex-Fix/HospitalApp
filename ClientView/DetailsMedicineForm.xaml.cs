using Data;
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
    /// Interaction logic for DetailsMedicineForm.xaml
    /// </summary>
    public partial class DetailsMedicineForm : Window
    {
        public DetailsMedicineForm(Medicine medicine)
        {
            InitializeComponent();
            NameLabel.Content += medicine.Name;
            SkuLabel.Content += medicine.Sku;
            PriceLabel.Content += medicine.Price.ToString();
            IndicationLabel.Content += medicine.Indication;
            CountryLabel.Content += medicine.Country.Name;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
