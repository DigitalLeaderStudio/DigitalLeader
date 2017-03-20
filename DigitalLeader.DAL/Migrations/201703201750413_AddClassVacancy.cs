namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassVacancy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vacancies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(),
                        Bonuses = c.String(),
                        Requirments = c.String(),
                        Responsibilities = c.String(),
                        WeOffer = c.String(),
                        IsPositionOpen = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TechnologiesWithVacancies",
                c => new
                    {
                        TechnologyId = c.Int(nullable: false),
                        VacancyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TechnologyId, t.VacancyId })
                .ForeignKey("dbo.Vacancies", t => t.TechnologyId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.VacancyId, cascadeDelete: true)
                .Index(t => t.TechnologyId)
                .Index(t => t.VacancyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TechnologiesWithVacancies", "VacancyId", "dbo.Technologies");
            DropForeignKey("dbo.TechnologiesWithVacancies", "TechnologyId", "dbo.Vacancies");
            DropIndex("dbo.TechnologiesWithVacancies", new[] { "VacancyId" });
            DropIndex("dbo.TechnologiesWithVacancies", new[] { "TechnologyId" });
            DropTable("dbo.TechnologiesWithVacancies");
            DropTable("dbo.Vacancies");
        }
    }
}
