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
    /// Interaction logic for DetailsPatientForm.xaml
    /// </summary>
    public partial class DetailsPatientForm : Window
    {
        public DetailsPatientForm(Patient patient)
        {
            InitializeComponent();
            FirstNameLabel.Content += patient.FirstName;
            LastNameLabel.Content += patient.LastName;
            MiddleNameLabel.Content += patient.MiddleName;
            AddressLabel.Content += patient.Address;
            InsurancePolicyLabel.Content += patient.InsurancePolicy;
            DateOfBirthLabel.Content += patient.DateOfBirth.ToShortDateString();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
