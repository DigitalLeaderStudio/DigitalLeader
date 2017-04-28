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

	public class BlogpostService : IBlogpostService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public BlogpostService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public Expression<Func<Blogpost, object>>[] Includes
		{
			get
			{
				return new Expression<Func<Blogpost, object>>[]
				{
					blogpost => blogpost.Author,
					blogpost => blogpost.Author.Technologies,
					blogpost => blogpost.Service
				};
			}
		}

		public List<Blogpost> GetAll()
		{
			return GetAllInclude(Includes);
		}

		public List<Blogpost> GetAllByService(int serviceId)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Blogpost>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query
					.Where(b => b.ServiceId == serviceId)
					.ToList();
			}
		}

		public List<Blogpost> GetAllInclude(params Expression<Func<Blogpost, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Blogpost>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public Blogpost GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Blogpost>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.ID == id);
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
	}
}
