namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendProjectWithLogo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "LogoId", c => c.Int());
            CreateIndex("dbo.Projects", "LogoId");
            AddForeignKey("dbo.Projects", "LogoId", "dbo.Files", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "LogoId", "dbo.Files");
            DropIndex("dbo.Projects", new[] { "LogoId" });
            DropColumn("dbo.Projects", "LogoId");
        }
    }
}
