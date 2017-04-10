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

	public class ContentService : IContentService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ContentService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public Expression<Func<Content, object>>[] Includes
		{
			get
			{
				return new Expression<Func<Content, object>>[]
				{
				};
			}
		}

		public List<Content> GetAll()
		{
			return GetAllInclude(Includes);
		}

		public List<Content> GetAllInclude(params Expression<Func<Content, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Content>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public Content GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Content>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.ID == id);
			}
		}

		public Content GetByKey(string key)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Content>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.Key == key);
			}
		}

		public void Update(Content value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Content>().SingleOrDefault(c => c.ID == value.ID);

				existed.Description = value.Description;
				existed.Html = value.Html;
				existed.IsActive = value.IsActive;
				existed.Key = value.Key;
				existed.Html = value.Html;
				existed.Keywords = value.Keywords;

				scope.SaveChanges();
			}
		}

		public void Insert(Content value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Content>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Content value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Content>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<Content>().Remove(existed);

				scope.SaveChanges();
			}
		}
	}
}
