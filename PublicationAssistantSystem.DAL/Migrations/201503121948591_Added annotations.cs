namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedannotations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition");
            AlterColumn("dbo.Employee", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Employee", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.PublicationBase", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Journal", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Institute", "Name", c => c.String(nullable: false));
            AddForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition", "ISSN", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition");
            AlterColumn("dbo.Institute", "Name", c => c.String());
            AlterColumn("dbo.Journal", "Title", c => c.String());
            AlterColumn("dbo.PublicationBase", "Title", c => c.String());
            AlterColumn("dbo.Employee", "LastName", c => c.String());
            AlterColumn("dbo.Employee", "FirstName", c => c.String());
            AddForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition", "ISSN");
        }
    }
}
