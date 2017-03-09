namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceCategoriesUpd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Services", "ServiceSubcategory_ID", "dbo.ServiceSubcategories");
            DropForeignKey("dbo.ServiceSubcategories", "ServiceCategory_ID", "dbo.ServiceCategories");
            DropIndex("dbo.Services", new[] { "ServiceSubcategory_ID" });
            DropIndex("dbo.ServiceSubcategories", new[] { "ServiceCategory_ID" });
            RenameColumn(table: "dbo.Services", name: "ServiceSubcategory_ID", newName: "ServiceSubcategoryID");
            RenameColumn(table: "dbo.ServiceSubcategories", name: "ServiceCategory_ID", newName: "ServiceCategoryID");
            AlterColumn("dbo.Services", "ServiceSubcategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.ServiceSubcategories", "ServiceCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Services", "ServiceSubcategoryID");
            CreateIndex("dbo.ServiceSubcategories", "ServiceCategoryID");
            AddForeignKey("dbo.Services", "ServiceSubcategoryID", "dbo.ServiceSubcategories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ServiceSubcategories", "ServiceCategoryID", "dbo.ServiceCategories", "ID", cascadeDelete: true);
            DropColumn("dbo.Services", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Services", "CategoryID", c => c.Int(nullable: false));
            DropForeignKey("dbo.ServiceSubcategories", "ServiceCategoryID", "dbo.ServiceCategories");
            DropForeignKey("dbo.Services", "ServiceSubcategoryID", "dbo.ServiceSubcategories");
            DropIndex("dbo.ServiceSubcategories", new[] { "ServiceCategoryID" });
            DropIndex("dbo.Services", new[] { "ServiceSubcategoryID" });
            AlterColumn("dbo.ServiceSubcategories", "ServiceCategoryID", c => c.Int());
            AlterColumn("dbo.Services", "ServiceSubcategoryID", c => c.Int());
            RenameColumn(table: "dbo.ServiceSubcategories", name: "ServiceCategoryID", newName: "ServiceCategory_ID");
            RenameColumn(table: "dbo.Services", name: "ServiceSubcategoryID", newName: "ServiceSubcategory_ID");
            CreateIndex("dbo.ServiceSubcategories", "ServiceCategory_ID");
            CreateIndex("dbo.Services", "ServiceSubcategory_ID");
            AddForeignKey("dbo.ServiceSubcategories", "ServiceCategory_ID", "dbo.ServiceCategories", "ID");
            AddForeignKey("dbo.Services", "ServiceSubcategory_ID", "dbo.ServiceSubcategories", "ID");
        }
    }
}
