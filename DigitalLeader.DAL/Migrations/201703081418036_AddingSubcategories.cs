namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSubcategories : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Categories", newName: "ServiceCategories");
            DropForeignKey("dbo.Services", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Services", new[] { "CategoryID" });
            RenameColumn(table: "dbo.Blogposts", name: "Category_ID", newName: "ServiceCategory_ID");
            RenameIndex(table: "dbo.Blogposts", name: "IX_Category_ID", newName: "IX_ServiceCategory_ID");
            CreateTable(
                "dbo.ServiceSubcategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ServiceCategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceCategories", t => t.ServiceCategory_ID)
                .Index(t => t.ServiceCategory_ID);
            
            AddColumn("dbo.Services", "ServiceSubcategory_ID", c => c.Int());
            CreateIndex("dbo.Services", "ServiceSubcategory_ID");
            AddForeignKey("dbo.Services", "ServiceSubcategory_ID", "dbo.ServiceSubcategories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceSubcategories", "ServiceCategory_ID", "dbo.ServiceCategories");
            DropForeignKey("dbo.Services", "ServiceSubcategory_ID", "dbo.ServiceSubcategories");
            DropIndex("dbo.ServiceSubcategories", new[] { "ServiceCategory_ID" });
            DropIndex("dbo.Services", new[] { "ServiceSubcategory_ID" });
            DropColumn("dbo.Services", "ServiceSubcategory_ID");
            DropTable("dbo.ServiceSubcategories");
            RenameIndex(table: "dbo.Blogposts", name: "IX_ServiceCategory_ID", newName: "IX_Category_ID");
            RenameColumn(table: "dbo.Blogposts", name: "ServiceCategory_ID", newName: "Category_ID");
            CreateIndex("dbo.Services", "CategoryID");
            AddForeignKey("dbo.Services", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.ServiceCategories", newName: "Categories");
        }
    }
}
