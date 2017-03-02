namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.String(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.ImageId);
            
            AddColumn("dbo.Services", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.Blogposts", "Category_ID", c => c.Int());
            CreateIndex("dbo.Blogposts", "Category_ID");
            CreateIndex("dbo.Services", "CategoryID");
            AddForeignKey("dbo.Services", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Blogposts", "Category_ID", "dbo.Categories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Blogposts", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Services", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Services", new[] { "CategoryID" });
            DropIndex("dbo.Blogposts", new[] { "Category_ID" });
            DropIndex("dbo.Categories", new[] { "ImageId" });
            DropColumn("dbo.Blogposts", "Category_ID");
            DropColumn("dbo.Services", "CategoryID");
            DropTable("dbo.Categories");
        }
    }
}
