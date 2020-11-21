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
                        DischargeDate = c.DateTime(),
                        Diagnosis = c.String(nullable: false),
                        WardId = c.Int(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PatientId)
                .Index(t => t.WardId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        Specialization = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sku = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Indication = c.String(nullable: false),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        MiddleName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        InsurancePolicy = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Wards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WardNumber = c.Int(nullable: false),
                        ComfotId = c.Int(nullable: false),
                        NumberOfPaces = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Role_User_Mappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
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
                .Index(t => t.Medicine_Id)
                .Index(t => t.Admission_Id);
            
        }
        
        public override void Down()
        {
             DropIndex("dbo.MedicineAdmissions", new[] { "Admission_Id" });
            DropIndex("dbo.MedicineAdmissions", new[] { "Medicine_Id" });
            DropIndex("dbo.Role_User_Mappings", new[] { "RoleId" });
            DropIndex("dbo.Role_User_Mappings", new[] { "UserId" });
            DropIndex("dbo.Medicines", new[] { "CountryId" });
            DropIndex("dbo.Admissions", new[] { "DoctorId" });
            DropIndex("dbo.Admissions", new[] { "WardId" });
            DropIndex("dbo.Admissions", new[] { "PatientId" });
            DropTable("dbo.MedicineAdmissions");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Role_User_Mappings");
            DropTable("dbo.Wards");
            DropTable("dbo.Patients");
            DropTable("dbo.Countries");
            DropTable("dbo.Medicines");
            DropTable("dbo.Doctors");
            DropTable("dbo.Admissions");
        }
    }
}
