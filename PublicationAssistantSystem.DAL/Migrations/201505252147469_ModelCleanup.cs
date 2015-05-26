namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelCleanup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Division", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Institute", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Faculty", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Faculty", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Institute", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Division", "Name", c => c.String());
        }
    }
}
