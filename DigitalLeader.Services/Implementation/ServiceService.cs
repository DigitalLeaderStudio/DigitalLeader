namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class ServiceService : BaseService, IServiceService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ServiceService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Entities.Service> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Service>().ToList();
			}
		}

		public Entities.Service GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Service>().SingleOrDefault(s => s.ID == id);
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

				existed.Image = HandleFile(existed.Image, value.Image);

				scope.SaveChanges();
			}
		}

		public void Insert(Entities.Service value)
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


		public List<Service> GetAllInclude(params System.Linq.Expressions.Expression<System.Func<Service, object>>[] includes)
		{
			throw new System.NotImplementedException();
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


		public System.Linq.Expressions.Expression<System.Func<Service, object>>[] Includes
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
