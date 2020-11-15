using Data;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class AddPatientForm : Form
    {
        private readonly TcpService tcpService;
        public AddPatientForm()
        {
            InitializeComponent();
            tcpService = new TcpService();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = this.FirstNameTextBox.Text;
                string middleName = this.MiddleNameTextBox.Text;
                string lastName = this.LastNameTextBox.Text;
                string address = this.AddressTextBox.Text;
                string insurancePolicy = this.InsurancePolicyTextBox.Text;
                DateTime dateOfBirth = this.DateOfBirthPicker.Value;

                //validation
                if (string.IsNullOrWhiteSpace(firstName))
                    throw new ArgumentException("'firstName' is incorrect");
                if (string.IsNullOrWhiteSpace(middleName))
                    throw new ArgumentException("'middleName' is incorrect");
                if (string.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentException("'lastName' is incorrect");
                if (string.IsNullOrWhiteSpace(address))
                    throw new ArgumentException("'address' is incorrect");
                if (string.IsNullOrWhiteSpace(insurancePolicy))
                    throw new ArgumentException("'insurancePolicy' is incorrect");
                if (dateOfBirth < new DateTime(1900, 1, 1) || dateOfBirth > DateTime.Now)
                    throw new ArgumentException("'dateOfBirth' is incorrect");

                var queryPatient = new Patient
                {
                    FirstName = firstName,
                    LastName = lastName,
                    MiddleName = middleName,
                    Address = address,
                    InsurancePolicy = insurancePolicy,
                    DateOfBirth = dateOfBirth
                };


                string request = tcpService.SerializeAddPatientRequest(queryPatient);
                byte[] data = tcpService.CodeStream(request);
                Program.stream.Write(data, 0, data.Length);
                string response = tcpService.DecodeStream(Program.stream);
                string[] responseArgs = response.Split(';');
                if (responseArgs.Length == 0 || responseArgs[0].Equals("500"))
                {
                    if(responseArgs.Length == 2)
                    {
                        throw new Exception(responseArgs[1]);
                    }
                    throw new Exception("Error!");
                }

                this.Close();
            }
            catch(Exception ex)
            {
                this.StatusLabel.Text = "Status: " + ex.Message;
            }
        }
    }
}
