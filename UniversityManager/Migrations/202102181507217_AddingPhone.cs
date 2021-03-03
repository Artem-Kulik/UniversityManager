namespace UniversityManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Phone");
        }
    }
}
