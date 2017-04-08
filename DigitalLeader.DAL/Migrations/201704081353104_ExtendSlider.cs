namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendSlider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "BackgroundStyle", c => c.String());
            AddColumn("dbo.Sliders", "HasImage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "HasImage");
            DropColumn("dbo.Sliders", "BackgroundStyle");
        }
    }
}
