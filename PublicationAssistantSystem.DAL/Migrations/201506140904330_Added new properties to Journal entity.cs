namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddednewpropertiestoJournalentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journal", "MNISZWPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journal", "MNISZWPoints");
        }
    }
}
