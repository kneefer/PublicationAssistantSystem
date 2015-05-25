namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modifiedstructureofdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition");
            DropIndex("dbo.PublicationBase", new[] { "Journal_ISSN" });
            RenameColumn(table: "dbo.PublicationBase", name: "Journal_ISSN", newName: "Journal_Id");
            DropPrimaryKey("dbo.JournalEdition");
            AddColumn("dbo.JournalEdition", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.JournalEdition", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.JournalEdition", "VolumeNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Journal", "ISSN", c => c.String(nullable: false));
            AddColumn("dbo.Journal", "eISSN", c => c.String());
            AddColumn("dbo.Journal", "IsOnMNISZW", c => c.Boolean(nullable: false));
            AddColumn("dbo.Journal", "IsOnWOS", c => c.Boolean(nullable: false));
            AddColumn("dbo.Journal", "IsOnJCR", c => c.Boolean(nullable: false));
            AlterColumn("dbo.PublicationBase", "PublicationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PublicationBase", "Journal_Id", c => c.Int());
            AddPrimaryKey("dbo.JournalEdition", "Id");
            CreateIndex("dbo.PublicationBase", "Journal_Id");
            AddForeignKey("dbo.PublicationBase", "Journal_Id", "dbo.JournalEdition", "Id", cascadeDelete: true);
            DropColumn("dbo.PublicationBase", "IsOnMNISZW");
            DropColumn("dbo.PublicationBase", "IsOnWOS");
            DropColumn("dbo.PublicationBase", "IsOnJCR");
            DropColumn("dbo.JournalEdition", "ISSN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalEdition", "ISSN", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PublicationBase", "IsOnJCR", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublicationBase", "IsOnWOS", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublicationBase", "IsOnMNISZW", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PublicationBase", "Journal_Id", "dbo.JournalEdition");
            DropIndex("dbo.PublicationBase", new[] { "Journal_Id" });
            DropPrimaryKey("dbo.JournalEdition");
            AlterColumn("dbo.PublicationBase", "Journal_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.PublicationBase", "PublicationDate", c => c.DateTime());
            DropColumn("dbo.Journal", "IsOnJCR");
            DropColumn("dbo.Journal", "IsOnWOS");
            DropColumn("dbo.Journal", "IsOnMNISZW");
            DropColumn("dbo.Journal", "eISSN");
            DropColumn("dbo.Journal", "ISSN");
            DropColumn("dbo.JournalEdition", "VolumeNumber");
            DropColumn("dbo.JournalEdition", "PublishDate");
            DropColumn("dbo.JournalEdition", "Id");
            AddPrimaryKey("dbo.JournalEdition", "ISSN");
            RenameColumn(table: "dbo.PublicationBase", name: "Journal_Id", newName: "Journal_ISSN");
            CreateIndex("dbo.PublicationBase", "Journal_ISSN");
            AddForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition", "ISSN", cascadeDelete: true);
        }
    }
}
