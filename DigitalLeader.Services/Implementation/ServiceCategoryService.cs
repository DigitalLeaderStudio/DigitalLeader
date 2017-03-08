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

	public class ServiceCategoryService : BaseService, IServiceCategoryService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ServiceCategoryService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<ServiceCategory> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<ServiceCategory>().Include(c => c.ServiceSubcategories).ToList();
			}
		}

		public List<ServiceCategory> GetAllInclude(params Expression<Func<ServiceCategory, object>>[] includes)
		{

			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<ServiceCategory>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}

		}

		public ServiceCategory GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext
					.Set<ServiceCategory>()
					.Include(c => c.ServiceSubcategories)
					.SingleOrDefault(c => c.ID == id);
			}
		}

		public void Update(ServiceCategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ServiceCategory>().SingleOrDefault(c => c.ID == value.ID);

				existed.Content = value.Content;
				existed.Name = value.Name;

				existed.Image = HandleFile(existed.Image, value.Image);

				scope.SaveChanges();
			}
		}

		public void Insert(ServiceCategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<ServiceCategory>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(ServiceCategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ServiceCategory>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<ServiceCategory>().Remove(existed);

				scope.SaveChanges();
			}
		}
	}
}
