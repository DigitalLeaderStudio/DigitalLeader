namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSEO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SEOs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Keywords = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SEOs");
        }
    }
}
