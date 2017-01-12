namespace DigitalLeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogModelUpdatedcategorytableadded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Blogs", newName: "Blogposts");
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Blogposts", "CategoryID", c => c.String());
            AddColumn("dbo.Blogposts", "Category_ID", c => c.Int());
            CreateIndex("dbo.Blogposts", "Category_ID");
            AddForeignKey("dbo.Blogposts", "Category_ID", "dbo.Categories", "ID");
            DropColumn("dbo.Blogposts", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogposts", "Category", c => c.String());
            DropForeignKey("dbo.Blogposts", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Blogposts", new[] { "Category_ID" });
            DropColumn("dbo.Blogposts", "Category_ID");
            DropColumn("dbo.Blogposts", "CategoryID");
            DropTable("dbo.Categories");
            RenameTable(name: "dbo.Blogposts", newName: "Blogs");
        }
    }
}
