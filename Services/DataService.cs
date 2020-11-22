﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.SqlClient;

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
            return _dbContext.Users.Where(el => el.Login.Equals(login) && el.Password.Equals(password)).Include(el => el.Role_User_Mappings.Select(s => s.Role.Role_User_Mappings)).FirstOrDefault();
        }

        public void InsertPatient(Patient patient)
        {
            var patientInDb = _dbContext.Patients.FirstOrDefault(el => el.InsurancePolicy.Equals(patient.InsurancePolicy));
            if(patientInDb != null)
            {
                throw new ArgumentException("The patient with an insurance policy exists in the database");
            }
            var query = @"INSERT INTO Patients (FirstName, LastName, MiddleName, Address, InsurancePolicy, DateOfBirth) VALUES (@FirstName, @LastName, @MiddleName, @Address, @InsurancePolicy, @DateOfBirth)";
            _dbContext.Database.ExecuteSqlCommand(query,
                new SqlParameter("@FirstName", patient.FirstName),
                new SqlParameter("@LastName", patient.LastName),
                new SqlParameter("@MiddleName", patient.MiddleName),
                new SqlParameter("@Address", patient.Address),
                new SqlParameter("@InsurancePolicy", patient.InsurancePolicy),
                new SqlParameter("@DateOfBirth", patient.DateOfBirth));
        }

        public void InsertDoctor(Doctor doctor)
        {
            var doctorInDb = _dbContext.Doctors.FirstOrDefault(el => el.Phone.Equals(doctor.Phone));
            if(doctorInDb != null)
            {
                throw new ArgumentException("The doctor with an telephone number exists in the database");
            }
            var query = @"INSERT INTO Doctors (FirstName, LastName, MiddleName, Phone, Specialization, DateOfBirth) VALUES (@FirstName, @LastName, @MiddleName, @Phone, @Specialization, @DateOfBirth)";
            _dbContext.Database.ExecuteSqlCommand(query,
                new SqlParameter("@FirstName", doctor.FirstName),
                new SqlParameter("@LastName", doctor.LastName),
                new SqlParameter("@MiddleName", doctor.MiddleName),
                new SqlParameter("@Phone", doctor.Phone),
                new SqlParameter("@Specialization", doctor.Specialization),
                new SqlParameter("@DateOfBirth", doctor.DateOfBirth));
        }

        public void InsertUser(User user)
        {
            var userInDb = _dbContext.Users.FirstOrDefault(el => el.Login.Equals(user.Login));
            if (userInDb != null)
            {
                throw new ArgumentException("The user with an login exists in the database");
            }
            var query = @"INSERT INTO Users (Login, Password) VALUES (@Login, @Password)";
            _dbContext.Database.ExecuteSqlCommand(query, new SqlParameter("@Login", user.Login), new SqlParameter("@Password", user.Password));
            var userId = _dbContext.Users.FirstOrDefault(el => el.Login.Equals(user.Login)).Id;
            foreach(var role in user.Role_User_Mappings)
            {
                query = @"INSERT INTO Role_User_Mappings (RoleId, UserId) VALUES (@RoleId, @UserId)";
                _dbContext.Database.ExecuteSqlCommand(query, new SqlParameter("@RoleId", role.Role.Id), new SqlParameter("@UserId", userId));
            }
        }

        public void InsertWard(Ward ward)
        {
            var wardInDb = _dbContext.Wards.FirstOrDefault(el => el.WardNumber == ward.WardNumber);
            if (wardInDb != null)
            {
                throw new ArgumentException("The ward with an wardNumber exists in the database");
            }
            var query = @"INSERT INTO Wards (WardNumber, ComfotId, NumberOfPaces) VALUES (@WardNumber, @ComfotId, @NumberOfPaces)";
            _dbContext.Database.ExecuteSqlCommand(query, new SqlParameter("@WardNumber", ward.WardNumber), new SqlParameter("@ComfotId", ward.ComfotId)
                , new SqlParameter("@NumberOfPaces", ward.NumberOfPaces));
        }

        public void InsertMedicine(Medicine medicine)
        {
            var medicineInDb = _dbContext.Medicines.FirstOrDefault(el => el.Sku.Equals(medicine.Sku));
            if(medicineInDb != null)
            {
                throw new ArgumentException("The medicine with an sku exists in the database");
            }
            var country = _dbContext.Countries.FirstOrDefault(el => el.Name.Equals(medicine.Country.Name));
            string query;
            if(country == null)
            {
                query = "INSERT INTO Countries (Name) VALUES (@Name)";
                _dbContext.Database.ExecuteSqlCommand(query, new SqlParameter("@Name", medicine.Country.Name));
                country = _dbContext.Countries.FirstOrDefault(el => el.Name.Equals(medicine.Country.Name));
            }
            query = "INSERT INTO Medicines (Sku, Name, Price, Indication, CountryId) VALUES (@Sku, @Name, @Price, @Indication, @CountryId)";
            _dbContext.Database.ExecuteSqlCommand(query,
                new SqlParameter("@Sku", medicine.Sku),
                new SqlParameter("@Name", medicine.Name),
                new SqlParameter("@Price", medicine.Price),
                new SqlParameter("@Indication", medicine.Indication),
                new SqlParameter("@CountryId", country.Id));
        }

        public List<Ward> GetAllFreeWards()
        {
            return _dbContext.Wards.Include(el => el.Admissions).Where(el => el.Admissions.Count() < el.NumberOfPaces).ToList();
        }

        public void InsertAdmission(Admission admission)
        {
            var query = "INSERT INTO Admissions (PatientId, DateOfReceipt, Diagnosis, WardId, DoctorId) VALUES (@PatientId ,@DateOfReceipt ,@Diagnosis ,@WardId ,@DoctorId )";
            _dbContext.Database.ExecuteSqlCommand(query,
                new SqlParameter("@PatientId", admission.PatientId),
                new SqlParameter("@DateOfReceipt", admission.DateOfReceipt),
                new SqlParameter("@Diagnosis", admission.Diagnosis),
                new SqlParameter("@WardId", admission.WardId),
                new SqlParameter("@DoctorId", admission.DoctorId));
        }

        public List<Patient> GetAllPatientsAndInfoAboutIt()
        {
            return _dbContext.Patients.Include(el => el.Admissions).ToList();
        }

        public void DeletePatient(int id)
        {
            var patient = _dbContext.Patients.FirstOrDefault(el => el.Id == id);
            if(patient==null)
               throw new ArgumentException("The patient does not exist");
            var query = "DELETE FROM Patients WHERE Id = @Id";
            _dbContext.Database.ExecuteSqlCommand(query,
                new SqlParameter("@Id", id));
        }

    }
}
