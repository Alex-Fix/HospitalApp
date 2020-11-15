namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertTestData : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO Roles (RoleName) VALUES ('Admin'), ('User')
GO
INSERT INTO Users (Login, Password) VALUES ('Alex','123'), ('Fix','123')
GO
INSERT INTO Role_User_Mapping (UserId, RoleId) VALUES (1, 1),(1, 2), (2,2)
GO
");
        }
        
        public override void Down()
        {
        }
    }
}
