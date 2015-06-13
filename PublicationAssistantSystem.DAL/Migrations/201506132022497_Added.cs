namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journal", "IsComputing", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journal", "IsComputing");
        }
    }
}
