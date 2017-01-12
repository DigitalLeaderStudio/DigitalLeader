namespace DigitalLeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.Int(nullable: false),
                        PublishedOn = c.DateTime(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Views = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blogs");
        }
    }
}
