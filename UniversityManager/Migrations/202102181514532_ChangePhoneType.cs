namespace UniversityManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePhoneType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Phone", c => c.Int(nullable: false));
        }
    }
}
