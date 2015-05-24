namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Madesomepropertiesrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Faculty", "Abbreviation", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Faculty", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Faculty", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Faculty", "Abbreviation", c => c.String(maxLength: 10));
        }
    }
}
