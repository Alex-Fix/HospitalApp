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
    /// Interaction logic for DetailsDoctorForm.xaml
    /// </summary>
    public partial class DetailsDoctorForm : Window
    {
        public DetailsDoctorForm(Doctor doctor)
        {
            InitializeComponent();
            FirstNameLabel.Content += doctor.FirstName;
            LastNameLabel.Content += doctor.LastName;
            MiddleNameLabel.Content += doctor.MiddleName;
            PhoneLabel.Content += doctor.Phone;
            SpecializationLabel.Content += doctor.Specialization;
            DateOfBirthLabel.Content += doctor.DateOfBirth.ToShortDateString();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
