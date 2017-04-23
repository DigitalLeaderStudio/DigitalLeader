namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public class SEOService : ISEOService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public SEOService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		#region Not implemented
		public Expression<Func<SEO, object>>[] Includes => throw new NotImplementedException();

		public List<SEO> GetAllInclude(params Expression<Func<SEO, object>>[] includes)
		{
			throw new NotImplementedException();
		}
		#endregion

		public List<SEO> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<SEO>().AsQueryable();

				return query.ToList();
			}
		}

		public SEO GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<SEO>().AsQueryable();

				return query.SingleOrDefault(c => c.ID == id);
			}
		}

		public SEO GetByKey(string key)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<SEO>().AsQueryable();

				return query.SingleOrDefault(c => c.Key == key);
			}
		}

		public void Delete(SEO value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<SEO>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<SEO>().Remove(existed);

				scope.SaveChanges();
			}
		}

		public void Insert(SEO value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<SEO>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Update(SEO value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<SEO>()
					.SingleOrDefault(c => c.ID == value.ID);

				existed.Key = value.Key;
				existed.Description = value.Description;
				existed.Keywords = value.Keywords;

				scope.SaveChanges();
			}
		}
	}
}
