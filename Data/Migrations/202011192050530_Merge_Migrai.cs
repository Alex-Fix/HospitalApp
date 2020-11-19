namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Merge_Migrai : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Role_User_Mapping", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Role_User_Mapping", "UserId", "dbo.Users");
            DropIndex("dbo.Role_User_Mapping", new[] { "UserId" });
            DropIndex("dbo.Role_User_Mapping", new[] { "RoleId" });
            AlterColumn("dbo.Role_User_Mapping", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Role_User_Mapping", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Role_User_Mapping", "UserId");
            CreateIndex("dbo.Role_User_Mapping", "RoleId");
            AddForeignKey("dbo.Role_User_Mapping", "RoleId", "dbo.Roles", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Role_User_Mapping", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Role_User_Mapping", "UserId", "dbo.Users");
            DropForeignKey("dbo.Role_User_Mapping", "RoleId", "dbo.Roles");
            DropIndex("dbo.Role_User_Mapping", new[] { "RoleId" });
            DropIndex("dbo.Role_User_Mapping", new[] { "UserId" });
            AlterColumn("dbo.Role_User_Mapping", "RoleId", c => c.Int());
            AlterColumn("dbo.Role_User_Mapping", "UserId", c => c.Int());
            CreateIndex("dbo.Role_User_Mapping", "RoleId");
            CreateIndex("dbo.Role_User_Mapping", "UserId");
            AddForeignKey("dbo.Role_User_Mapping", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Role_User_Mapping", "RoleId", "dbo.Roles", "Id");
        }
    }
}
