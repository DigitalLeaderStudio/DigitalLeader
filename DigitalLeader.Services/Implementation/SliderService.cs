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

	public class SliderService : BaseService, ISliderService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public SliderService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public Expression<Func<Slider, object>>[] Includes
		{
			get
			{
				return new Expression<Func<Slider, object>>[]
				{
					//client => client.Image
				};
			}
		}

		public List<Slider> GetAll()
		{
			return GetAllInclude(Includes);
		}

		public List<Slider> GetAllInclude(params Expression<Func<Slider, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Slider>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public Slider GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Slider>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.ID == id);
			}
		}

		public void Update(Slider value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Slider>().Find(value.ID);

				existed.Image = HandleFile(existed.Image, value.Image);
				existed.Description = value.Description;
				existed.TargetLink = value.TargetLink;
				existed.Title = value.Title;

				scope.SaveChanges();
			}
		}

		public void Insert(Slider value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Sliders.Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Slider value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Sliders.Find(value.ID);

				dbContext.Sliders.Remove(existed);

				scope.SaveChanges();
			}
		}
	}
}
