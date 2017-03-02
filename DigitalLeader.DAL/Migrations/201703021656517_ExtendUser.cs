namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Biography", c => c.String());
            AddColumn("dbo.Users", "Title", c => c.String());
            AddColumn("dbo.Users", "ExperianceYears", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "ImageId", c => c.Int());
            CreateIndex("dbo.Users", "ImageId");
            AddForeignKey("dbo.Users", "ImageId", "dbo.Files", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ImageId", "dbo.Files");
            DropIndex("dbo.Users", new[] { "ImageId" });
            DropColumn("dbo.Users", "ImageId");
            DropColumn("dbo.Users", "ExperianceYears");
            DropColumn("dbo.Users", "Title");
            DropColumn("dbo.Users", "Biography");
        }
    }
}
