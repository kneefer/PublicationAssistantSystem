namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDivisionIdparametertoEmployeeDAO : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employee", "Division_Id", "dbo.Division");
            DropIndex("dbo.Employee", new[] { "Division_Id" });
            RenameColumn(table: "dbo.Employee", name: "Division_Id", newName: "DivisionId");
            AlterColumn("dbo.Employee", "DivisionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employee", "DivisionId");
            AddForeignKey("dbo.Employee", "DivisionId", "dbo.Division", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DivisionId", "dbo.Division");
            DropIndex("dbo.Employee", new[] { "DivisionId" });
            AlterColumn("dbo.Employee", "DivisionId", c => c.Int());
            RenameColumn(table: "dbo.Employee", name: "DivisionId", newName: "Division_Id");
            CreateIndex("dbo.Employee", "Division_Id");
            AddForeignKey("dbo.Employee", "Division_Id", "dbo.Division", "Id");
        }
    }
}
