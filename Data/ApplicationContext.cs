using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Role_User_Mapping> Role_User_Mapings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ward> Wards { get; set; }

        public ApplicationContext() : base("HospitalDB")
        {}
    }
}
