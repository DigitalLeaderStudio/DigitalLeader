namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFieldName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Projects", new[] { "ClientId" });
            CreateIndex("dbo.Projects", "ClientID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Projects", new[] { "ClientID" });
            CreateIndex("dbo.Projects", "ClientId");
        }
    }
}
