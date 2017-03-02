namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTesimonials : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID)
                .Index(t => t.ClientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Testimonials", "ClientID", "dbo.Clients");
            DropIndex("dbo.Testimonials", new[] { "ClientID" });
            DropTable("dbo.Testimonials");
        }
    }
}
