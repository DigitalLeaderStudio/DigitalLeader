namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	public class UserService : IUserService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public UserService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<User> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<User>().ToList();
			}
		}

		public List<User> GetAllInclude(params Expression<Func<User, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<User>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public User GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<User>().SingleOrDefault(u => u.Id == id);
			}
		}

		public List<User> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<User>()
					.Where(t => ids.Contains(t.Id))
					.ToList();
			}
		}

		public void Update(User value)
		{
			throw new NotImplementedException();
		}

		public void Insert(User value)
		{
			throw new NotImplementedException();
		}

		public void Delete(User value)
		{
			throw new NotImplementedException();
		}
	}
}
