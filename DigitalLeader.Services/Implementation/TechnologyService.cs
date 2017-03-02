namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class TechnologyService : ITechnologyService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public TechnologyService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Technology> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Technology>().ToList();
			}
		}

		public Technology GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Technology>().SingleOrDefault(t => t.ID == id);
			}
		}

		public void Update(Technology value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Technology>().SingleOrDefault(t => t.ID == value.ID);

				existed.Name = value.Name;

				scope.SaveChanges();
			}
		}

		public void Insert(Technology value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Technology>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Technology value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Technology>().SingleOrDefault(t => t.ID == value.ID);

				dbContext.Set<Technology>().Remove(existed);

				scope.SaveChanges();
			}
		}

		public List<Technology> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Technology>()
					.Where(t => ids.Contains(t.ID))
					.ToList();
			}
		}


		public List<Technology> GetAllInclude(params System.Linq.Expressions.Expression<System.Func<Technology, object>>[] includes)
		{
			throw new System.NotImplementedException();
		}
	}
}
