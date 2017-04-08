namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalizedProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        LocaleKeyGroup = c.String(),
                        LocaleKey = c.String(),
                        LocaleValue = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LocalizedProperties");
        }
    }
}
