namespace DigitalLeader.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Company = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Title = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Industries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ProjectUrl = c.String(),
                        Kewywords = c.String(),
                        Overview = c.String(),
                        Objective = c.String(),
                        WorkOverview = c.String(),
                        ResultOverview = c.String(),
                        IsCaseStudy = c.Boolean(nullable: false),
                        IsFeatured = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClientId = c.Int(nullable: false),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.ClientId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubTitle = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.ImageId);
            
            CreateTable(
                "dbo.Blogposts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PublishedDate = c.DateTime(nullable: false),
                        Title = c.String(),
                        Overview = c.String(),
                        Content = c.String(),
                        Keywords = c.String(),
                        ViewsCount = c.Int(nullable: false),
                        IsPublished = c.Boolean(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.AuthorId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ClientsWithIndustries",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        IndustryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientId, t.IndustryId })
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Industries", t => t.IndustryId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.IndustryId);
            
            CreateTable(
                "dbo.EmployeesWithServices",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ServiceId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.EmployeesWithTechnologies",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TechnologyId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TechnologyId);
            
            CreateTable(
                "dbo.ContributorsWithProjects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.UserId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ProjectsWithProjects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.ServiceId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.TechnologiesWithProjects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        TechnologyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectId, t.TechnologyId })
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Technologies", t => t.TechnologyId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.TechnologyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.TechnologiesWithProjects", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.TechnologiesWithProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ProjectsWithProjects", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ProjectsWithProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ImageId", "dbo.Files");
            DropForeignKey("dbo.ContributorsWithProjects", "UserId", "dbo.Users");
            DropForeignKey("dbo.ContributorsWithProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.EmployeesWithTechnologies", "TechnologyId", "dbo.Technologies");
            DropForeignKey("dbo.EmployeesWithTechnologies", "UserId", "dbo.Users");
            DropForeignKey("dbo.EmployeesWithServices", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.EmployeesWithServices", "UserId", "dbo.Users");
            DropForeignKey("dbo.Services", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Blogposts", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Blogposts", "AuthorId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Projects", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientsWithIndustries", "IndustryId", "dbo.Industries");
            DropForeignKey("dbo.ClientsWithIndustries", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "ImageId", "dbo.Files");
            DropIndex("dbo.TechnologiesWithProjects", new[] { "TechnologyId" });
            DropIndex("dbo.TechnologiesWithProjects", new[] { "ProjectId" });
            DropIndex("dbo.ProjectsWithProjects", new[] { "ServiceId" });
            DropIndex("dbo.ProjectsWithProjects", new[] { "ProjectId" });
            DropIndex("dbo.ContributorsWithProjects", new[] { "UserId" });
            DropIndex("dbo.ContributorsWithProjects", new[] { "ProjectId" });
            DropIndex("dbo.EmployeesWithTechnologies", new[] { "TechnologyId" });
            DropIndex("dbo.EmployeesWithTechnologies", new[] { "UserId" });
            DropIndex("dbo.EmployeesWithServices", new[] { "ServiceId" });
            DropIndex("dbo.EmployeesWithServices", new[] { "UserId" });
            DropIndex("dbo.ClientsWithIndustries", new[] { "IndustryId" });
            DropIndex("dbo.ClientsWithIndustries", new[] { "ClientId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.Blogposts", new[] { "ServiceId" });
            DropIndex("dbo.Blogposts", new[] { "AuthorId" });
            DropIndex("dbo.Services", new[] { "ImageId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Projects", new[] { "ImageId" });
            DropIndex("dbo.Projects", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "ImageId" });
            DropTable("dbo.TechnologiesWithProjects");
            DropTable("dbo.ProjectsWithProjects");
            DropTable("dbo.ContributorsWithProjects");
            DropTable("dbo.EmployeesWithTechnologies");
            DropTable("dbo.EmployeesWithServices");
            DropTable("dbo.ClientsWithIndustries");
            DropTable("dbo.Roles");
            DropTable("dbo.Technologies");
            DropTable("dbo.Blogposts");
            DropTable("dbo.Services");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.Industries");
            DropTable("dbo.Files");
            DropTable("dbo.Clients");
        }
    }
}
