namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeClientTOProjectTestimonials : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Testimonials", name: "ClientID", newName: "Client_ID");
            RenameIndex(table: "dbo.Testimonials", name: "IX_ClientID", newName: "IX_Client_ID");
            AddColumn("dbo.Testimonials", "ProjectID", c => c.Int());
            CreateIndex("dbo.Testimonials", "ProjectID");
            AddForeignKey("dbo.Testimonials", "ProjectID", "dbo.Projects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Testimonials", new[] { "ProjectID" });
            DropColumn("dbo.Testimonials", "ProjectID");
            RenameIndex(table: "dbo.Testimonials", name: "IX_Client_ID", newName: "IX_ClientID");
            RenameColumn(table: "dbo.Testimonials", name: "Client_ID", newName: "ClientID");
        }
    }
}
