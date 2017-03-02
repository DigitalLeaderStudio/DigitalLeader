namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using DigitalLeader.Services.Extensions;
	using DigitalLeader.Entities.Identity;

	public class ProjectService : BaseService, IProjectService
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

				return dbContext.Set<Project>()
					.Include(p => p.Client)
					.Include(p => p.Technologies)
					.Include(p => p.Services)
					.Include(p => p.Contributors)
					.ToList();
			}
		}

		public Project GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Projects
					.Include(p => p.Client)
					.Include(p => p.Technologies)
					.Include(p => p.Services)
					.Include(p => p.Contributors)
					.First(p => p.ID == id);
			}
		}

		public void Update(Project value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = GetById(value.ID);

				existed.ClientID = value.ClientID;
				existed.EndDate = value.EndDate;
				existed.IsCaseStudy = value.IsCaseStudy;
				existed.IsFeatured = value.IsFeatured;
				existed.Kewywords = value.Kewywords;
				existed.Objective = value.Objective;
				existed.Overview = value.Overview;
				existed.ProjectUrl = value.ProjectUrl;
				existed.ResultOverview = value.ResultOverview;
				existed.StartDate = value.StartDate;
				existed.Title = value.Title;
				existed.WorkOverview = value.WorkOverview;

				existed.Image = HandleFile(existed.Image, value.Image);

				existed.Technologies = HandleCollection<Technology, int>(
					existed.Technologies.ToList(),
					value.Technologies.ToList(),
					tech => tech.ID,
					dbContext);

				existed.Services = HandleCollection<Service, int>(
					existed.Services.ToList(),
					value.Services.ToList(),
					srv => srv.ID,
					dbContext);

				existed.Contributors = HandleCollection<User, int>(
					existed.Contributors.ToList(),
					value.Contributors.ToList(),
					user => user.Id,
					dbContext);

				scope.SaveChanges();
			}
		}

		public void Insert(Project value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				value.Technologies = HandleCollection<Technology>(value.Technologies.ToList(), dbContext);
				value.Contributors = HandleCollection<User>(value.Contributors.ToList(), dbContext);
				value.Services	= HandleCollection<Service>(value.Services.ToList(), dbContext);

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

		public List<Project> GetAllInclude(params System.Linq.Expressions.Expression<System.Func<Project, object>>[] includes)
		{
			throw new System.NotImplementedException();
		}
	}
}
