using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Data;
using System.Reflection;

namespace Services
{
    public class TcpService
    {
        private readonly DataService dataService;
        public TcpService(ApplicationContext context = null)
        {
            if (context != null)
                dataService = new DataService(context);
        }


        public string DecodeAndProcessRequest(string request)
        {
            var socketNode = JsonSerializer.Deserialize<SocketNode>(request);
            TcpMethods tcpMethod;
            string response = "";
            if (!Enum.TryParse<TcpMethods>(socketNode.Method, out tcpMethod))
            {
                tcpMethod = TcpMethods.NONE;
            }


            switch (tcpMethod)
            {
                case TcpMethods.Authorize:
                    response = Authorize(socketNode);
                    break;
                case TcpMethods.AddPatient:
                    response = AddPatient(socketNode);
                    break;
                case TcpMethods.AddDoctor:
                    response = AddDoctor(socketNode);
                    break;
                case TcpMethods.InitRolesInForm:
                    response = InitRolesInForm();
                    break;
                case TcpMethods.AddUser:
                    response = AddUser(socketNode);
                    break;
                case TcpMethods.AddWard:
                    response = AddWard(socketNode);
                    break;
                case TcpMethods.AddMedicine:
                    response = AddMedicine(socketNode);
                    break;
                case TcpMethods.InitModelsInAddAdmissionForm:
                    response = InitModelsInAddAdmissionForm();
                    break;
                case TcpMethods.AddAdmission:
                    response = AddAdmission(socketNode);
                    break;
                case TcpMethods.InitPatientsInViewPatientForm:
                    response = InitPatientsInViewPatientForm();
                    break;
                case TcpMethods.DeletePatient:
                    response = DeletePatient(socketNode);
                    break;
                case TcpMethods.InitDoctorsInViewDoctorsForm:
                    response = InitDoctorsInViewDoctorsForm();
                    break;
                case TcpMethods.DeleteDoctor:
                    response = DeleteDoctor(socketNode);
                    break;
                case TcpMethods.InitUsersInViewUsersForm:
                    response = InitUsersInViewUsersForm();
                    break;
                case TcpMethods.DeleteUser:
                    response = DeleteUser(socketNode);
                    break;
                case TcpMethods.InitMedicinesInViewMedicinesForm:
                    response = InitMedicinesInViewMedicinesForm();
                    break;
                case TcpMethods.DeleteMedicine:
                    response = DeleteMedicine(socketNode);
                    break;
                case TcpMethods.InitAdmissionsInViewAdmissionsForm:
                    response = InitAdmissionsInViewAdmissionsForm();
                    break;
                case TcpMethods.DeleteAdmission:
                    response = DeleteAdmission(socketNode);
                    break;
                case TcpMethods.MoveMedicinesToAdmission:
                    response = MoveMedicinesToAdmission(socketNode);
                    break;
                case TcpMethods.CloseAdmission:
                    response = CloseAdmission(socketNode);
                    break;
                case TcpMethods.InitWardsInViewWardsForm:
                    response = InitWardsInViewWardsForm();
                    break;
                case TcpMethods.DeleteWard:
                    response = DeleteWard(socketNode);
                    break;
                default:
                    break;
            }

            return response;
        }

        public async Task<string> DecodeStreamAsync(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = await stream.ReadAsync(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        public string DecodeStream(NetworkStream stream)
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }

        public async Task<byte[]> CodeStreamAsync(string request)
        {
            return await Task.Run(() => CodeStream(request));
        }

        public byte[] CodeStream(string request)
        {
            return Encoding.UTF8.GetBytes(request);
        }

        public string SerializeAuthorizeRequest(string login, string password)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "Authorize",
                User = JsonSerializer.Serialize<User>(new User
                {
                    Login = login,
                    Password = password
                })
            });
        }

        public User DeserializeAuthorizeResponse(string response)
        {
            return JsonSerializer.Deserialize<User>(response);
        }

        private string Authorize(SocketNode node)
        {
            string response = "";
            User requestUser = JsonSerializer.Deserialize<User>(node.User);

            if (requestUser != null)
            {
                User user = dataService.GetUser(requestUser.Login, requestUser.Password);
                if (user == null)
                {
                    user = new User();
                }
                else
                {
                    foreach (var el in user.Role_User_Mappings)
                    {
                        el.User = null;
                        el.Role.Role_User_Mappings = null;
                    }
                }
                response = JsonSerializer.Serialize<User>(user);
            }

            return response;
        }

        public string SerializeAddPatientRequest(Patient patient, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddPatient",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Patient>(patient)
            });
        }

        private string AddPatient(SocketNode socketNode)
        {
            try
            {
                var requestPatient = JsonSerializer.Deserialize<Patient>(socketNode.Args);
                dataService.InsertPatient(requestPatient);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }


        public string SerializeAddDoctorRequest(Doctor doctor, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddDoctor",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Doctor>(doctor)
            });
        }

        private string AddDoctor(SocketNode socketNode)
        {
            try
            {
                var requestDoctor = JsonSerializer.Deserialize<Doctor>(socketNode.Args);
                dataService.InsertDoctor(requestDoctor);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitRolesInFormRequest(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode {
                Method = "InitRolesInForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Role> DeserializeInitRolesInFormResponse(string response)
        {
            return JsonSerializer.Deserialize<List<Role>>(response);
        }

        private string InitRolesInForm()
        {
            try
            {
                List<Role> roles = dataService.GetAllRoles();
                return JsonSerializer.Serialize<List<Role>>(roles.Select(x => new Role {
                    RoleName = x.RoleName,
                    Id = x.Id
                }).ToList());
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeAddUserRequest(User newUser, User curUser)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddUser",
                User = JsonSerializer.Serialize<User>(curUser),
                Args = JsonSerializer.Serialize<User>(newUser)
            });
        }

        private string AddUser(SocketNode socketNode)
        {
            try
            {
                User user = JsonSerializer.Deserialize<User>(socketNode.Args);
                dataService.InsertUser(user);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }


        public string SerializeAddWardRequest(Ward ward, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode {
                Method = "AddWard",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Ward>(ward)
            });
        }

        private string AddWard(SocketNode socketNode)
        {
            try
            {
                Ward ward = JsonSerializer.Deserialize<Ward>(socketNode.Args);
                dataService.InsertWard(ward);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeAddMedicineRequest(Medicine medicine, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "AddMedicine",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Medicine>(medicine)
            });
        }

        private string AddMedicine(SocketNode socketNode)
        {
            try
            {
                Medicine medicine = JsonSerializer.Deserialize<Medicine>(socketNode.Args);
                dataService.InsertMedicine(medicine);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public List<Patient> InitPatientsInForm(string serializePatients)
        {
            return JsonSerializer.Deserialize<List<Patient>>(serializePatients);
        }

        public List<Ward> InitWardsInForm(string serializeWards)
        {
            return JsonSerializer.Deserialize<List<Ward>>(serializeWards);
        }

        public List<Doctor> InitDoctorsInForm(string serializeDoctors)
        {
            return JsonSerializer.Deserialize<List<Doctor>>(serializeDoctors);
        }

        public string SerializeInitModelsInAddAdmissionForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitModelsInAddAdmissionForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<string> DeseializeInitModelsInAddAdmissionForm(string response)
        {
            return JsonSerializer.Deserialize<List<string>>(response);
        }

        private string InitModelsInAddAdmissionForm()
        {
            try
            {
                var patients = dataService.GetAllPatientsAndInfoAboutIt();
                var wards = dataService.GetAllFreeWardsAndInfoAboutIt();
                var doctors = dataService.GetAllDoctorsAndInfoAboutIt();
                foreach (var el in patients)
                {
                    el.Admissions = null;
                }
                foreach (var el in wards)
                {
                    el.Admissions = null;
                }
                foreach (var el in doctors)
                {
                    el.Admissions = null;
                }
                return JsonSerializer.Serialize<List<string>>(new List<string> {
                    JsonSerializer.Serialize<List<Patient>>(patients),
                    JsonSerializer.Serialize<List<Ward>>(wards),
                    JsonSerializer.Serialize<List<Doctor>>(doctors)
                });
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeAddAdmissionRequest(Admission admission, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode {
                Method = "AddAdmission",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Admission>(admission)
            });
        }


        private string AddAdmission(SocketNode socketNode)
        {
            try
            {
                Admission admission = JsonSerializer.Deserialize<Admission>(socketNode.Args);
                dataService.InsertAdmission(admission);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }


        public string SerializeInitPatientsInViewPatientForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitPatientsInViewPatientForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Patient> DeseializeInitPatientsInViewPatientForm(string response)
        {
            return JsonSerializer.Deserialize<List<Patient>>(response);
        }

        private string InitPatientsInViewPatientForm()
        {
            try
            {
                var patients = dataService.GetAllPatientsAndInfoAboutIt();
                foreach (var patient in patients)
                {
                    patient.Admissions = null;
                }
                return JsonSerializer.Serialize<List<Patient>>(patients);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeletePatient(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "DeletePatient",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        private string DeletePatient(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeletePatient(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitDoctorsInViewDoctorsForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitDoctorsInViewDoctorsForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Doctor> DeseializeInitDoctorsInViewDoctorsForm(string response)
        {
            return JsonSerializer.Deserialize<List<Doctor>>(response);
        }

        private string InitDoctorsInViewDoctorsForm()
        {
            try
            {
                var doctors = dataService.GetAllDoctorsAndInfoAboutIt();
                foreach (var doctor in doctors)
                {
                    doctor.Admissions = null;
                }
                return JsonSerializer.Serialize<List<Doctor>>(doctors);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeleteDoctor(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "DeleteDoctor",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        private string DeleteDoctor(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeleteDoctor(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitUsersInViewUsersForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitUsersInViewUsersForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<User> DeseializeInitUserInViewUsersForm(string response)
        {
            return JsonSerializer.Deserialize<List<User>>(response);
        }

        private string InitUsersInViewUsersForm()
        {
            try
            {
                var users = dataService.GetAllUsersAndInfoAboutIt();
                foreach (var user in users)
                {
                    foreach (var mepping in user.Role_User_Mappings)
                    {
                        mepping.User = null;
                        mepping.Role.Role_User_Mappings = null;
                    }
                }
                return JsonSerializer.Serialize<List<User>>(users);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeleteUser(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "DeleteUser",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        private string DeleteUser(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeleteUser(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitMedicinesInViewMedicinesForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitMedicinesInViewMedicinesForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Medicine> DeseializeInitMedicinesInViewMedicinesForm(string response)
        {
            return JsonSerializer.Deserialize<List<Medicine>>(response);
        }

        private string InitMedicinesInViewMedicinesForm()
        {
            try
            {
                var medicines = dataService.GetAllMedicinesAndInfoAboutIt();
                foreach (var medicine in medicines)
                {
                    medicine.Admissions = null;
                    medicine.Country.Medicines = null;
                }
                return JsonSerializer.Serialize<List<Medicine>>(medicines);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeleteMedicine(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "DeleteMedicine",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        private string DeleteMedicine(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeleteMedicine(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitAdmissionsInViewAdmissionsForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitAdmissionsInViewAdmissionsForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Admission> DeseializeInitAdmissionsInViewAdmissionsForm(string response)
        {
            return JsonSerializer.Deserialize<List<Admission>>(response);
        }

        private string InitAdmissionsInViewAdmissionsForm()
        {
            try
            {
                var admissions = dataService.GetAllAdmissionsAndInfoAboutIt();
                foreach (var admission in admissions)
                {
                    if (admission.Doctor != null)
                        admission.Doctor.Admissions = null;
                    if (admission.Patient != null)
                        admission.Patient.Admissions = null;
                    if (admission.Ward != null)
                        admission.Ward.Admissions = null;
                    if (admission.Medisines != null)
                    {
                        foreach (var medicine in admission.Medisines)
                        {
                            if (medicine != null)
                            {
                                medicine.Admissions = null;
                                if (medicine.Country != null)
                                    medicine.Country.Medicines = null;
                            }
                        }
                    }
                }
                return JsonSerializer.Serialize<List<Admission>>(admissions);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeleteAdmission(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "DeleteAdmission",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        public string DeleteAdmission(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeleteAdmission(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeMoveMedicinesToAdmission(Admission admission, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "MoveMedicinesToAdmission",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Admission>(admission)
            });
        }

        private string MoveMedicinesToAdmission(SocketNode socketNode)
        {
            try
            {
                Admission admission = JsonSerializer.Deserialize<Admission>(socketNode.Args);
                dataService.InsertMedicineToAdmission(admission.Id, admission.Medisines.Select(el => el.Id).ToList());
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeCloseAdmission(Admission admission, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "CloseAdmission",
                User = JsonSerializer.Serialize<User>(user),
                Args = JsonSerializer.Serialize<Admission>(admission)
            });
        }

        private string CloseAdmission(SocketNode socketNode)
        {
            try
            {
                Admission admission = JsonSerializer.Deserialize<Admission>(socketNode.Args);
                dataService.CloseAdmission(admission.Id, admission.DischargeDate.Value);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeInitWardsInViewWardsForm(User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode
            {
                Method = "InitWardsInViewWardsForm",
                User = JsonSerializer.Serialize<User>(user)
            });
        }

        public List<Ward> DeseializeInitWardsInViewWardsForm(string response)
        {
            return JsonSerializer.Deserialize<List<Ward>>(response);
        }

        public string InitWardsInViewWardsForm()
        {
            try
            {
                var wards = dataService.GetAllWardsAndInfoAboutIt();
                foreach (var ward in wards)
                {
                    ward.Admissions = null;
                }
                return JsonSerializer.Serialize<List<Ward>>(wards);
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

        public string SerializeDeleteWard(int id, User user)
        {
            return JsonSerializer.Serialize<SocketNode>(new SocketNode { 
                Method = "DeleteWard",
                User = JsonSerializer.Serialize<User>(user),
                Args = id.ToString()
            });
        }

        private string DeleteWard(SocketNode socketNode)
        {
            try
            {
                int id = JsonSerializer.Deserialize<int>(socketNode.Args);
                dataService.DeleteWard(id);
                return "200";
            }
            catch (Exception ex)
            {
                return "500;" + ex.Message;
            }
        }

    }

    

    public enum TcpMethods
    {
        NONE,
        Authorize,
        AddPatient,
        AddDoctor,
        InitRolesInForm,
        AddUser,
        AddWard,
        AddMedicine,
        InitModelsInAddAdmissionForm,
        AddAdmission,
        InitPatientsInViewPatientForm,
        DeletePatient,
        InitDoctorsInViewDoctorsForm,
        DeleteDoctor,
        InitUsersInViewUsersForm,
        DeleteUser,
        InitMedicinesInViewMedicinesForm,
        DeleteMedicine,
        InitAdmissionsInViewAdmissionsForm,
        DeleteAdmission,
        MoveMedicinesToAdmission,
        CloseAdmission,
        InitWardsInViewWardsForm,
        DeleteWard
    }
}
