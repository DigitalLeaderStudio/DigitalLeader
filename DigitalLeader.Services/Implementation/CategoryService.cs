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

	public class CategoryService : BaseService, ICategoryService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public CategoryService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Category> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Category>().Include(c => c.Services).ToList();
			}
		}

		public List<Category> GetAllInclude(params Expression<Func<Category, object>>[] includes)
		{

			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Category>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}

		}

		public Category GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Category>().SingleOrDefault(c => c.ID == id);
			}
		}

		public void Update(Category value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Category>().SingleOrDefault(c => c.ID == value.ID);

				existed.Content = value.Content;
				existed.Name = value.Name;

				existed.Image = HandleFile(existed.Image, value.Image);

				scope.SaveChanges();
			}
		}

		public void Insert(Category value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Category>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Category value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Category>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<Category>().Remove(existed);

				scope.SaveChanges();
			}
		}
	}
}
