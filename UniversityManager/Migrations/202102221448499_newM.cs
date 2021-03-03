namespace UniversityManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "ApplicationUserId" });
            AddColumn("dbo.Students", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Students", "ApplicationUserId", c => c.String());
            CreateIndex("dbo.Students", "ApplicationUser_Id");
            AddForeignKey("dbo.Students", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.Students", "ApplicationUserId", c => c.String(maxLength: 128));
            DropColumn("dbo.Students", "ApplicationUser_Id");
            CreateIndex("dbo.Students", "ApplicationUserId");
            AddForeignKey("dbo.Students", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
