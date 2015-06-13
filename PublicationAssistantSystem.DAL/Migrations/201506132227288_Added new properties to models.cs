namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addednewpropertiestomodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicationBase", "IsComputing", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublicationBase", "IsOnWOS", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublicationBase", "IsOnWOS");
            DropColumn("dbo.PublicationBase", "IsComputing");
        }
    }
}
