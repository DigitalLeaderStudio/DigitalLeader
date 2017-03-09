namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class BlogpostService : IBlogpostService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public BlogpostService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Blogpost> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Blogpost>().ToList();
			}
		}

		public Blogpost GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Blogpost>().SingleOrDefault(c => c.ID == id);
			}
		}

		public void Update(Blogpost value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Blogpost>().SingleOrDefault(c => c.ID == value.ID);

				existed.Content = value.Content;
				existed.IsPublished = value.IsPublished;
				existed.Keywords = value.Keywords;
				existed.Overview = value.Overview;
				existed.PublishedDate = value.PublishedDate;
				existed.Title = value.Title;
				existed.ServiceId = value.ServiceId;

				//existed.Author = value.Author;
				//existed.Service = value.Service;

				//existed.ViewsCount = value.ViewsCount;
				//HandleFile(existed.Image, value.Image);

				scope.SaveChanges();
			}
		}

		public void Insert(Blogpost value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Blogpost>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Blogpost value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Blogpost>().SingleOrDefault(c => c.ID == value.ID);

				dbContext.Set<Blogpost>().Remove(existed);

				scope.SaveChanges();
			}
		}


		public List<Blogpost> GetAllInclude(params System.Linq.Expressions.Expression<System.Func<Blogpost, object>>[] includes)
		{
			throw new System.NotImplementedException();
		}


		public System.Linq.Expressions.Expression<System.Func<Blogpost, object>>[] Includes
		{
			get { throw new System.NotImplementedException(); }
		}
	}
}
