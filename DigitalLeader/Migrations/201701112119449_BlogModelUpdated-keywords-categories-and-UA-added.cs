namespace DigitalLeader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogModelUpdatedkeywordscategoriesandUAadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Keywords", c => c.String());
            AddColumn("dbo.Blogs", "Category", c => c.String());
            AddColumn("dbo.Blogs", "TitleUA", c => c.String());
            AddColumn("dbo.Blogs", "ContentUA", c => c.String());
            AddColumn("dbo.Blogs", "KeywordsUA", c => c.String());
            AddColumn("dbo.Blogs", "CategoryUA", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "CategoryUA");
            DropColumn("dbo.Blogs", "KeywordsUA");
            DropColumn("dbo.Blogs", "ContentUA");
            DropColumn("dbo.Blogs", "TitleUA");
            DropColumn("dbo.Blogs", "Category");
            DropColumn("dbo.Blogs", "Keywords");
        }
    }
}
