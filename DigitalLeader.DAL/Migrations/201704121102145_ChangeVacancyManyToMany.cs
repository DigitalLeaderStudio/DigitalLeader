namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVacancyManyToMany : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "TechnologyId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "VacancyId", newName: "TechnologyId");
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "__mig_tmp__0", newName: "VacancyId");
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "IX_TechnologyId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "IX_VacancyId", newName: "IX_TechnologyId");
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "__mig_tmp__0", newName: "IX_VacancyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "IX_VacancyId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "IX_TechnologyId", newName: "IX_VacancyId");
            RenameIndex(table: "dbo.TechnologiesWithVacancies", name: "__mig_tmp__0", newName: "IX_TechnologyId");
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "VacancyId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "TechnologyId", newName: "VacancyId");
            RenameColumn(table: "dbo.TechnologiesWithVacancies", name: "__mig_tmp__0", newName: "TechnologyId");
        }
    }
}
