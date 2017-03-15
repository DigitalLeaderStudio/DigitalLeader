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

	public class ServiceSubcategoryService : BaseService, IServiceSubcategoryService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ServiceSubcategoryService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

        public Expression<Func<ServiceSubcategory, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ServiceSubcategory, object>>[] {
                    serviceSubcategory => serviceSubcategory.Services,
                    serviceSucategory => serviceSucategory.ServiceCategory
                };
            }
        }

        public List<ServiceSubcategory> GetAll()
		{
            return GetAllInclude(Includes);
		}

		public List<ServiceSubcategory> GetAllInclude(params Expression<Func<ServiceSubcategory, object>>[] includes)
		{

			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<ServiceSubcategory>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}

		}

		public ServiceSubcategory GetById(int id)
		{
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<ServiceSubcategory>().AsQueryable();

                if (Includes != null)
                {
                    query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.SingleOrDefault(c => c.ID == id);
            }
        }

		public void Update(ServiceSubcategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ServiceSubcategory>().SingleOrDefault(c => c.ID == value.ID);

				existed.Name = value.Name;
                existed.ServiceCategoryID = value.ServiceCategoryID;

				scope.SaveChanges();
			}
		}

		public void Insert(ServiceSubcategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<ServiceSubcategory>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(ServiceSubcategory value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ServiceSubcategory>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<ServiceSubcategory>().Remove(existed);

				scope.SaveChanges();
			}
		}
	}
}
