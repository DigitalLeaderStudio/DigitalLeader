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

    public class ServiceService : BaseService, IServiceService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ServiceService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

        public Expression<Func<Service, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Service, object>>[]
                {
                    x => x.ServiceSubcategory,
                    x => x.Blogposts,
                    x => x.Employees,
                    x => x.Projects
                };
            }
        }


        public List<Service> GetAllInclude(params Expression<Func<Service, object>>[] includes)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Service>().AsQueryable();

                if (includes != null)
                {
                    query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.ToList();
            }
        }

        public List<Entities.Service> GetAll()
        {
            return GetAllInclude(Includes);
        }

		public Service GetById(int id)
		{
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Service>().AsQueryable();

                if (Includes != null)
                {
                    query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.SingleOrDefault(c => c.ID == id);
            }
        }

		public void Update(Entities.Service value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Service>().SingleOrDefault(s => s.ID == value.ID);

				existed.Content = value.Content;
				existed.Description = value.Description;
				existed.Title = value.Title;
                existed.SubTitle = value.SubTitle;
                existed.ServiceSubcategoryID = value.ServiceSubcategoryID;

				existed.Image = HandleFile(existed.Image, value.Image);

				scope.SaveChanges();
			}
		}

		public void Insert(Service value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Service>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Entities.Service value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Service>().SingleOrDefault(s => s.ID == value.ID);

				dbContext.Set<Service>().Remove(existed);

				scope.SaveChanges();
			}
		}

		public Service GetByName(string name)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Service>().SingleOrDefault(s => s.Title == name);
			}
		}
		
		public List<Service> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Service>()
					.Where(t => ids.Contains(t.ID))
					.ToList();
			}
		}
	}
}
