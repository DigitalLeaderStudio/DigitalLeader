namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class ProjectService : IProjectService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ProjectService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Project> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Projects.Include(p => p.Client).ToList();
			}
		}

		public Project GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Projects.Include(p => p.Client).First(p => p.ID == id);
			}
		}

		public void Update(Project value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existedProject = dbContext
					.Projects.Find(value.ID);

				existedProject.ClientID = value.ClientID;
				//existedProject.Contributors = value.Contributors;
				existedProject.EndDate = value.EndDate;
				existedProject.Image = value.Image;
				existedProject.IsCaseStudy = value.IsCaseStudy;
				existedProject.IsFeatured = value.IsFeatured;
				existedProject.Kewywords = value.Kewywords;
				existedProject.Objective = value.Objective;
				existedProject.Overview = value.Overview;
				existedProject.ProjectUrl = value.ProjectUrl;
				existedProject.ResultOverview = value.ResultOverview;
				existedProject.Services = value.Services;
				existedProject.StartDate = value.StartDate;
				existedProject.Technologies = value.Technologies;
				existedProject.Title = value.Title;
				existedProject.WorkOverview = value.WorkOverview;

				scope.SaveChanges();
			}
		}

		public void Insert(Project value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Projects.Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Entities.Project value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existedProject = dbContext.Projects.Find(value.ID);

				dbContext.Projects.Remove(existedProject);

				scope.SaveChanges();
			}
		}
	}
}
