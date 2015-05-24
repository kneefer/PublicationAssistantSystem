namespace PublicationAssistantSystem.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedabbreviationpropertytoFacultytable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faculty", "Abbreviation", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faculty", "Abbreviation");
        }
    }
}
