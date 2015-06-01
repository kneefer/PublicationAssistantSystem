namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoredDAOandaddednavigationIDproperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Division", "Institute_Id", "dbo.Institute");
            DropForeignKey("dbo.JournalEdition", "Journal_Id", "dbo.Journal");
            DropForeignKey("dbo.Institute", "Faculty_Id", "dbo.Faculty");
            DropIndex("dbo.Division", new[] { "Institute_Id" });
            DropIndex("dbo.JournalEdition", new[] { "Journal_Id" });
            DropIndex("dbo.Institute", new[] { "Faculty_Id" });
            RenameColumn(table: "dbo.Division", name: "Institute_Id", newName: "InstituteId");
            RenameColumn(table: "dbo.JournalEdition", name: "Journal_Id", newName: "JournalId");
            RenameColumn(table: "dbo.Institute", name: "Faculty_Id", newName: "FacultyId");
            AlterColumn("dbo.Division", "InstituteId", c => c.Int(nullable: false));
            AlterColumn("dbo.JournalEdition", "JournalId", c => c.Int(nullable: false));
            AlterColumn("dbo.Institute", "FacultyId", c => c.Int(nullable: false));
            CreateIndex("dbo.Division", "InstituteId");
            CreateIndex("dbo.JournalEdition", "JournalId");
            CreateIndex("dbo.Institute", "FacultyId");
            AddForeignKey("dbo.Division", "InstituteId", "dbo.Institute", "Id", cascadeDelete: true);
            AddForeignKey("dbo.JournalEdition", "JournalId", "dbo.Journal", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Institute", "FacultyId", "dbo.Faculty", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Institute", "FacultyId", "dbo.Faculty");
            DropForeignKey("dbo.JournalEdition", "JournalId", "dbo.Journal");
            DropForeignKey("dbo.Division", "InstituteId", "dbo.Institute");
            DropIndex("dbo.Institute", new[] { "FacultyId" });
            DropIndex("dbo.JournalEdition", new[] { "JournalId" });
            DropIndex("dbo.Division", new[] { "InstituteId" });
            AlterColumn("dbo.Institute", "FacultyId", c => c.Int());
            AlterColumn("dbo.JournalEdition", "JournalId", c => c.Int());
            AlterColumn("dbo.Division", "InstituteId", c => c.Int());
            RenameColumn(table: "dbo.Institute", name: "FacultyId", newName: "Faculty_Id");
            RenameColumn(table: "dbo.JournalEdition", name: "JournalId", newName: "Journal_Id");
            RenameColumn(table: "dbo.Division", name: "InstituteId", newName: "Institute_Id");
            CreateIndex("dbo.Institute", "Faculty_Id");
            CreateIndex("dbo.JournalEdition", "Journal_Id");
            CreateIndex("dbo.Division", "Institute_Id");
            AddForeignKey("dbo.Institute", "Faculty_Id", "dbo.Faculty", "Id");
            AddForeignKey("dbo.JournalEdition", "Journal_Id", "dbo.Journal", "Id");
            AddForeignKey("dbo.Division", "Institute_Id", "dbo.Institute", "Id");
        }
    }
}
