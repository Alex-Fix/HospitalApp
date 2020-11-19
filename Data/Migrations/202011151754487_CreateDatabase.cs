namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        DateOfReceipt = c.DateTime(nullable: false),
                        DischargeDate = c.DateTime(nullable: false),
                        Diagnosis = c.String(),
                        WardId = c.Int(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .ForeignKey("dbo.Wards", t => t.WardId)
                .Index(t => t.PatientId)
                .Index(t => t.WardId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Specialization = c.String(),
                        Phone = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Indication = c.String(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        Address = c.String(),
                        InsurancePolicy = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WardNumber = c.Int(nullable: false),
                        Comfot = c.Int(nullable: false),
                        NumberOfPaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role_User_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicineAdmissions",
                c => new
                    {
                        Medicine_Id = c.Int(nullable: true),
                        Admission_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicine_Id, t.Admission_Id })
                .ForeignKey("dbo.Medicines", t => t.Medicine_Id)
                .ForeignKey("dbo.Admissions", t => t.Admission_Id)
                .Index(t => t.Medicine_Id)
                .Index(t => t.Admission_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Role_User_Mapping", "UserId", "dbo.Users");
            DropForeignKey("dbo.Role_User_Mapping", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Admissions", "WardId", "dbo.Wards");
            DropForeignKey("dbo.Admissions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Medicines", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MedicineAdmissions", "Admission_Id", "dbo.Admissions");
            DropForeignKey("dbo.MedicineAdmissions", "Medicine_Id", "dbo.Medicines");
            DropForeignKey("dbo.Admissions", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.MedicineAdmissions", new[] { "Admission_Id" });
            DropIndex("dbo.MedicineAdmissions", new[] { "Medicine_Id" });
            DropIndex("dbo.Role_User_Mapping", new[] { "RoleId" });
            DropIndex("dbo.Role_User_Mapping", new[] { "UserId" });
            DropIndex("dbo.Medicines", new[] { "CountryId" });
            DropIndex("dbo.Admissions", new[] { "DoctorId" });
            DropIndex("dbo.Admissions", new[] { "WardId" });
            DropIndex("dbo.Admissions", new[] { "PatientId" });
            DropTable("dbo.MedicineAdmissions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Role_User_Mapping");
            DropTable("dbo.Wards");
            DropTable("dbo.Patients");
            DropTable("dbo.Countries");
            DropTable("dbo.Medicines");
            DropTable("dbo.Doctors");
            DropTable("dbo.Admissions");
        }
    }
}
