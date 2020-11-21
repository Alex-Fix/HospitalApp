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

        public ApplicationContext() : base(@"server=ALEXFIXPC;database=HospitalDB;trusted_connection=true;")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Admission
            modelBuilder.Entity<Admission>()
                .ToTable("Admissions");
            modelBuilder.Entity<Admission>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Admission>()
                .Property(a => a.DateOfReceipt).IsRequired();
            modelBuilder.Entity<Admission>()
                .Property(a => a.DischargeDate).IsOptional();
            modelBuilder.Entity<Admission>()
                .Property(a => a.Diagnosis).IsRequired();
            modelBuilder.Entity<Admission>()
                .HasOptional(a => a.Patient)
                .WithMany(a => a.Admissions)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Admission>()
                .HasOptional(a => a.Doctor)
                .WithMany(a=> a.Admissions)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Admission>()
                .HasOptional(a => a.Ward)
                .WithMany(a => a.Admissions)
                .WillCascadeOnDelete(false);

            //Country
            modelBuilder.Entity<Country>()
                .ToTable("Countries");
            modelBuilder.Entity<Country>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Country>()
                .Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Country>()
                .HasMany(a => a.Medicines)
                .WithOptional(a => a.Country)
                .WillCascadeOnDelete(false);

            //Doctor
            modelBuilder.Entity<Doctor>()
                .ToTable("Doctors");
            modelBuilder.Entity<Doctor>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Doctor>()
                .Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(a => a.MiddleName).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(a => a.Specialization).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(a => a.Phone).IsRequired();
            modelBuilder.Entity<Doctor>()
                .Property(a => a.DateOfBirth).IsRequired();
            modelBuilder.Entity<Doctor>()
                .HasMany(a => a.Admissions)
                .WithOptional(a => a.Doctor)
                .WillCascadeOnDelete(false);

            //Medicine
            modelBuilder.Entity<Medicine>()
                .ToTable("Medicines");
            modelBuilder.Entity<Medicine>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Medicine>()
                .Property(a => a.Sku).IsRequired();
            modelBuilder.Entity<Medicine>()
                .Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Medicine>()
                .Property(a => a.Price).IsRequired().HasPrecision(18, 2);
            modelBuilder.Entity<Medicine>()
                .Property(a => a.Indication).IsRequired();
            modelBuilder.Entity<Medicine>()
                .HasOptional(a => a.Country)
                .WithMany(a => a.Medicines)
                .WillCascadeOnDelete(false);

            //Patient
            modelBuilder.Entity<Patient>()
                .ToTable("Patients");
            modelBuilder.Entity<Patient>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Patient>()
                .Property(a => a.FirstName).IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(a => a.MiddleName).IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(a => a.LastName).IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(a => a.Address).IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(a => a.InsurancePolicy).IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(a => a.DateOfBirth).IsRequired();
            modelBuilder.Entity<Patient>()
                .HasMany(a => a.Admissions)
                .WithOptional(a => a.Patient)
                .WillCascadeOnDelete(false);

            //Role
            modelBuilder.Entity<Role>()
                .ToTable("Roles");
            modelBuilder.Entity<Role>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Role>()
                .Property(a => a.RoleName).IsRequired();
            modelBuilder.Entity<Role>()
                .HasMany(a => a.Role_User_Mappings)
                .WithRequired(a => a.Role)
                .WillCascadeOnDelete(false);


            //Role_User_Mapping
            modelBuilder.Entity<Role_User_Mapping>()
                .ToTable("Role_User_Mappings");
            modelBuilder.Entity<Role_User_Mapping>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Role_User_Mapping>()
                .HasRequired(a => a.User)
                .WithMany(a => a.Role_User_Mappings)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Role_User_Mapping>()
                .HasRequired(a => a.Role)
                .WithMany(a => a.Role_User_Mappings)
                .WillCascadeOnDelete(false);

            //User
            modelBuilder.Entity<User>()
                .ToTable("Users");
            modelBuilder.Entity<User>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<User>()
                .Property(a => a.Login).IsRequired();
            modelBuilder.Entity<User>()
                .Property(a => a.Password).IsRequired();
            modelBuilder.Entity<User>()
                .HasMany(a => a.Role_User_Mappings)
                .WithRequired(a => a.User)
                .WillCascadeOnDelete(false);

            //Ward
            modelBuilder.Entity<Ward>()
                .ToTable("Wards");
            modelBuilder.Entity<Ward>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Ward>()
                .Property(a => a.WardNumber).IsRequired();
            modelBuilder.Entity<Ward>()
                .Property(a => a.ComfotId).IsRequired();
            modelBuilder.Entity<Ward>()
                .Ignore(a => a.Comfort);
            modelBuilder.Entity<Ward>()
                .Property(a => a.NumberOfPaces).IsRequired();
            modelBuilder.Entity<Ward>()
                .HasMany(a => a.Admissions)
                .WithOptional(a => a.Ward)
                .WillCascadeOnDelete(false);
        }

    }
}
