namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO Users (Login, Password) VALUES ('Alex','123'),('Fix','123')");
            Sql(@"INSERT INTO Roles (RoleName) VALUES ('Admin'), ('User')");
            Sql(@"INSERT INTO Role_User_Mappings (UserId, RoleId) VALUES (1,1), (1,2),(2,2)");
        }

        public override void Down()
        {
        }
    }
}
