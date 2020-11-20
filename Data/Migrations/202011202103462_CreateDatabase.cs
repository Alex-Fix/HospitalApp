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
                    PatientId = c.Int(nullable: true),
                    DateOfReceipt = c.DateTime(nullable: false),
                    DischargeDate = c.DateTime(nullable: false),
                    Diagnosis = c.String(),
                    WardId = c.Int(nullable: true),
                    DoctorId = c.Int(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .ForeignKey("dbo.Wards", t => t.WardId);
            
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
                    CountryId = c.Int(nullable: true),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String()
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
                        Medicine_Id = c.Int(nullable: false),
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
        }
    }
}
