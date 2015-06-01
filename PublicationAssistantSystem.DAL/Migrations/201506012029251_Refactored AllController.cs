namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredAllController : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PublicationBase", name: "Journal_Id", newName: "JournalEditionId");
            RenameIndex(table: "dbo.PublicationBase", name: "IX_Journal_Id", newName: "IX_JournalEditionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PublicationBase", name: "IX_JournalEditionId", newName: "IX_Journal_Id");
            RenameColumn(table: "dbo.PublicationBase", name: "JournalEditionId", newName: "Journal_Id");
        }
    }
}
