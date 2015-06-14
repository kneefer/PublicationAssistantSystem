namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedmissingpropertytoJournalentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journal", "MNISZWList", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journal", "MNISZWList");
        }
    }
}
