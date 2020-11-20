using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace Services
{
    public class DataService
    {
        private readonly ApplicationContext _dbContext;
        public DataService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Country> GetAllCountries()
        {
            return _dbContext.Countries.ToList();
        }

        public List<Admission> GetAllAdmissions()
        {
            return _dbContext.Admissions.ToList();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _dbContext.Doctors.ToList();
        }

        public List<Medicine> GetAllMedicines()
        {
            return _dbContext.Medicines.ToList();
        }

        public List<Patient> GetAllPatients()
        {
            return _dbContext.Patients.ToList();
        }

        public List<Role> GetAllRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public List<Role_User_Mapping> GetAllRole_User_Mappings()
        {
            return _dbContext.Role_User_Mapings.ToList();
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public List<Ward> GetAllWards()
        {
            return _dbContext.Wards.ToList();
        }

        public User GetUser(string login, string password)
        {
            return _dbContext.Users.Where(el => el.Login.Equals(login) && el.Password.Equals(password)).Include(el => el.Role_User_Mappings.Select(s => s.Role)).FirstOrDefault();
        }

        public void InsertPatient(Patient patient)
        {
            var patientInDb = _dbContext.Patients.FirstOrDefault(el => el.InsurancePolicy.Equals(patient.InsurancePolicy));
            if(patientInDb != null)
            {
                throw new ArgumentException("The patient with an insurance policy exists in the database");
            }
            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();
        }

        public void InsertDoctor(Doctor doctor)
        {
            var doctorInDb = _dbContext.Doctors.FirstOrDefault(el => el.Phone.Equals(doctor.Phone));
            if(doctorInDb != null)
            {
                throw new ArgumentException("The doctor with an telephone number exists in the database");
            }
            _dbContext.Doctors.Add(doctor);
            _dbContext.SaveChanges();
        }
    }
}
