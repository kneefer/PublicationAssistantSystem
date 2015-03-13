namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanUp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Division",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Institute_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Institute", t => t.Institute_Id)
                .Index(t => t.Institute_Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcademicTitle = c.String(),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Division", t => t.Division_Id)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.PublicationBase",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        PublicationDate = c.DateTime(),
                        IsOnMNISZW = c.Boolean(nullable: false),
                        IsOnWOS = c.Boolean(nullable: false),
                        IsOnJCR = c.Boolean(nullable: false),
                        PageFrom = c.Int(),
                        PageTo = c.Int(),
                        ISBN = c.String(),
                        Publisher = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Journal_ISSN = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JournalEdition", t => t.Journal_ISSN, cascadeDelete: true)
                .Index(t => t.Journal_ISSN);
            
            CreateTable(
                "dbo.JournalEdition",
                c => new
                    {
                        ISSN = c.String(nullable: false, maxLength: 128),
                        Journal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ISSN)
                .ForeignKey("dbo.Journal", t => t.Journal_Id)
                .Index(t => t.Journal_Id);
            
            CreateTable(
                "dbo.Journal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Institute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Faculty_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculty", t => t.Faculty_Id)
                .Index(t => t.Faculty_Id);
            
            CreateTable(
                "dbo.Faculty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublicationBaseEmployee",
                c => new
                    {
                        PublicationBase_Id = c.Int(nullable: false),
                        Employee_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PublicationBase_Id, t.Employee_Id })
                .ForeignKey("dbo.PublicationBase", t => t.PublicationBase_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Employee_Id, cascadeDelete: true)
                .Index(t => t.PublicationBase_Id)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Institute", "Faculty_Id", "dbo.Faculty");
            DropForeignKey("dbo.Division", "Institute_Id", "dbo.Institute");
            DropForeignKey("dbo.PublicationBase", "Journal_ISSN", "dbo.JournalEdition");
            DropForeignKey("dbo.JournalEdition", "Journal_Id", "dbo.Journal");
            DropForeignKey("dbo.PublicationBaseEmployee", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.PublicationBaseEmployee", "PublicationBase_Id", "dbo.PublicationBase");
            DropForeignKey("dbo.Employee", "Division_Id", "dbo.Division");
            DropIndex("dbo.PublicationBaseEmployee", new[] { "Employee_Id" });
            DropIndex("dbo.PublicationBaseEmployee", new[] { "PublicationBase_Id" });
            DropIndex("dbo.Institute", new[] { "Faculty_Id" });
            DropIndex("dbo.JournalEdition", new[] { "Journal_Id" });
            DropIndex("dbo.PublicationBase", new[] { "Journal_ISSN" });
            DropIndex("dbo.Employee", new[] { "Division_Id" });
            DropIndex("dbo.Division", new[] { "Institute_Id" });
            DropTable("dbo.PublicationBaseEmployee");
            DropTable("dbo.Faculty");
            DropTable("dbo.Institute");
            DropTable("dbo.Journal");
            DropTable("dbo.JournalEdition");
            DropTable("dbo.PublicationBase");
            DropTable("dbo.Employee");
            DropTable("dbo.Division");
        }
    }
}
