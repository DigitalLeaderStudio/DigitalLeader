namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;


	public class TechnologyService : ITechnologyService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public TechnologyService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public Expression<Func<Technology, object>>[] Includes
		{
			get
			{
				return new Expression<Func<Technology, object>>[] {
					technology => technology.Projects,
					technology => technology.Employees
				};
			}
		}

		public List<Technology> GetAll()
		{
			return GetAllInclude(Includes);
		}

		public Technology GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Technology>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.ID == id);
			}
		}

		public List<Technology> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Technology>()
					.Where(t => ids.Contains(t.ID))
					.ToList();
			}
		}

		public List<Technology> GetAllInclude(params Expression<Func<Technology, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Technology>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
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
	}
}
