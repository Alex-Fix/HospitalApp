using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for DetalilsAdmissionForm.xaml
    /// </summary>
    public partial class DetalilsAdmissionForm : Window
    {
        private readonly TcpService tcpService;
        private static Admission selectedAdmission;
        private ObservableCollection<Medicine> allMedisines = new ObservableCollection<Medicine>();
        private ObservableCollection<Medicine> addmissionMedisines = new ObservableCollection<Medicine>();
        private bool IsClosed = false;

        public static async Task<DetalilsAdmissionForm> CreateAsyncDetailsAdmissionForm(Admission admission)
        {
            DetalilsAdmissionForm form = new DetalilsAdmissionForm();
            await form.InitializeAsync(admission);
            selectedAdmission = admission;
            return form;
        }

        private DetalilsAdmissionForm()
        {
            tcpService = new TcpService();
        }

        private async Task InitializeAsync(Admission admission)
        {
            try
            {
                InitializeComponent();
                var med = await InitModelsInForm();
                if (admission.Medisines != null)
                {
                    foreach (var el in admission.Medisines)
                    {
                        addmissionMedisines.Add(el);
                    }
                }
                AdmissionMedicineListBox.ItemsSource = addmissionMedisines;
                var ids = addmissionMedisines.Select(el => el.Id).ToList();
                foreach (var el in med)
                {
                    if(!ids.Contains(el.Id))
                        allMedisines.Add(el);
                }
                AllMedicineListBox.ItemsSource = allMedisines;
                
                PatientLabel.Content += admission.Patient?.FullName ?? "";
                DoctorLabel.Content += admission.Doctor?.FullName ?? "";
                WardLabel.Content += admission.Ward?.WardNumber.ToString() ?? "";
                DiagnosisLabel.Content += admission.Diagnosis ?? "";
                DateOfReceiptLabel.Content += admission.DateOfReceipt.ToString();
                DischargeDate.Content += admission.DischargeDate?.ToString() ?? "";
                SingletoneObj.DischargeDate = admission.DischargeDate;
                if (admission.DischargeDate.HasValue)
                {
                    CloseAdmissionBtn.IsEnabled = false;
                    IsClosed = true;
                }
                    
            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<List<Medicine>> InitModelsInForm()
        {
            try
            {
                string request = tcpService.SerializeInitMedicinesInViewMedicinesForm(SingletoneObj.User);
                byte[] data = await tcpService.CodeStreamAsync(request);
                await SingletoneObj.Stream.WriteAsync(data, 0, data.Length);
                string response = await tcpService.DecodeStreamAsync(SingletoneObj.Stream);
                List<Medicine> models = tcpService.DeseializeInitMedicinesInViewMedicinesForm(response);
                return models;
            }
            catch (Exception)
            {
                return new List<Medicine>();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void CloseAdmissionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var requestAdmission = new Admission
                {
                    Id = selectedAdmission.Id,
                    DischargeDate = DateTime.Now
                };

                string request = tcpService.SerializeCloseAdmission(requestAdmission, SingletoneObj.User);
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
                    selectedAdmission.DischargeDate = requestAdmission.DischargeDate;
                    DischargeDate.Content = requestAdmission.DischargeDate.ToString();
                    SingletoneObj.DischargeDate = requestAdmission.DischargeDate;
                    IsClosed = true;
                    var directory = Directory.GetCurrentDirectory() + @"\ClosedAdmissions";
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    var patientName = selectedAdmission.Patient?.FullName.Replace(' ', '_') ?? Guid.NewGuid().ToString().Replace(' ', '_');
                    string path = Directory.GetCurrentDirectory() + @"\ClosedAdmissions\" + patientName + selectedAdmission.DischargeDate.ToString().Replace(' ', '_').Replace(':', '_') + ".txt";
                    using (var stream = new StreamWriter(path))
                    {
                        string patient = "Пацієнт: " + selectedAdmission.Patient?.FullName ?? "noname";
                        string doctor = "Лікар: " + selectedAdmission.Doctor?.FullName ?? "noname";
                        string ward = "Палата: " + selectedAdmission.Ward?.WardNumber.ToString() ?? "none";
                        string diagosis = "Діагноз: " + selectedAdmission.Diagnosis ?? "none";
                        string dateOfReceipt = "Дата надходження: " + selectedAdmission.DateOfReceipt.ToString();
                        string dischargeDate = "Дата виписки: " + selectedAdmission.DischargeDate.ToString();
                        string medicines = "Ліки: " + string.Join("   ... , ...  ", selectedAdmission.Medisines.Select(el => el.Name));
                        string price = "Ціна: " + selectedAdmission.Medisines.Sum(el => el.Price).ToString() + " грн.";
                        stream.WriteLine(patient);
                        stream.WriteLine(doctor);
                        stream.WriteLine(ward);
                        stream.WriteLine(diagosis);
                        stream.WriteLine(dateOfReceipt);
                        stream.WriteLine(dischargeDate);
                        stream.WriteLine(medicines);
                        stream.WriteLine(price);
                    }
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                SingletoneObj.DischargeDate = null;
                StatusLabel.Content = "Status: " + ex.Message;
            }
            finally
            {
                ExitBtn.IsEnabled = true;
                MoveMedicineBtn.IsEnabled = true;
                CloseAdmissionBtn.IsEnabled = !IsClosed;
            }
        }

        private async void MoveMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ExitBtn.IsEnabled = false;
                MoveMedicineBtn.IsEnabled = false;
                CloseAdmissionBtn.IsEnabled = false;
                if(IsClosed)
                    throw new ArgumentException($"Addmition is closed");

                var selectedItems = AllMedicineListBox.SelectedItems.Cast<Medicine>().Select(el => el.Id).ToList();

                if(selectedItems.Count()==0)
                    throw new ArgumentException($"{nameof(selectedItems)} is incorrect");

                Admission requestAdmission = new Admission
                {
                    Id = selectedAdmission.Id,
                    Medisines = selectedItems.Select(x => new Medicine { Id = x }).ToList()
                };

                string request = tcpService.SerializeMoveMedicinesToAdmission(requestAdmission, SingletoneObj.User);
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
                    foreach(var item in selectedItems)
                    {
                        var admission = allMedisines.First(el => el.Id == item);
                        allMedisines.Remove(admission);
                        addmissionMedisines.Add(admission);
                        selectedAdmission.Medisines.Add(admission);
                    }
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Content = "Status: " + ex.Message; 
            }
            finally
            {
                ExitBtn.IsEnabled = true;
                MoveMedicineBtn.IsEnabled = true;
                CloseAdmissionBtn.IsEnabled = !IsClosed;
            }
        }
    }
}
