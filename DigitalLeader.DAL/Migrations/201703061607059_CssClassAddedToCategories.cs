namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CssClassAddedToCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CssClass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "CssClass");
        }
    }
}
