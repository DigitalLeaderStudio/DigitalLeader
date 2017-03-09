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

    public class TestimonialService : ITestimonialService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public TestimonialService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Expression<Func<Testimonial, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Testimonial, object>>[] {
                    testimonial => testimonial.Client
                };
            }
        }

        public List<Testimonial> GetAll()
		{
            return GetAllInclude(Includes);
		}

		public List<Testimonial> GetAllInclude(params Expression<Func<Testimonial, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<Testimonial>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public Testimonial GetById(int id)
		{
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Testimonial>().AsQueryable();

                if (Includes != null)
                {
                    query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.SingleOrDefault(c => c.ID == id);
            }
        }

		public List<Testimonial> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<Testimonial>()
					.Where(t => ids.Contains(t.ID))
					.ToList();
			}
		}

		public void Update(Testimonial value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Testimonial>().SingleOrDefault(t => t.ID == value.ID);

				existed.CreatedDate = value.CreatedDate;
				existed.ClientID = value.ClientID;
				existed.Text = value.Text;

				scope.SaveChanges();
			}
		}

		public void Insert(Testimonial value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<Testimonial>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Testimonial value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<Testimonial>().SingleOrDefault(t => t.ID == value.ID);

				dbContext.Set<Testimonial>().Remove(existed);

				scope.SaveChanges();
			}
		}
        	
	}
}
