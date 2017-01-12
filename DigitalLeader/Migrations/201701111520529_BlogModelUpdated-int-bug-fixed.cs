namespace DigitalLeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogModelUpdatedintbugfixed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "Content", c => c.Int(nullable: false));
        }
    }
}
