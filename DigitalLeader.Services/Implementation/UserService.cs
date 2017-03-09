namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;
	using System.Linq.Expressions;

	public class UserService : BaseService, IUserService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public UserService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

        public Expression<Func<User, object>>[] Includes
        {
            get
            {
                return new Expression<Func<User, object>>[]
                {
                    user => user.Image,
                    user => user.Services,
                    user => user.Blogposts,
                    user => user.Technologies,
                    user => user.Projects
                };
            }
        }

        public List<User> GetAll()
		{
            return GetAllInclude(Includes);
		}

		public List<User> GetAllInclude(params Expression<Func<User, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<User>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public User GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<User>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.Id == id);
			}
		}

		public List<User> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<User>()
					.Where(t => ids.Contains(t.Id))
					.ToList();
			}
		}

		public void Update(User value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<User>().SingleOrDefault(u => u.Id == value.Id);

				existed.Image = HandleFile(existed.Image, value.Image);

				existed.Biography = value.Biography;
				existed.Email = value.Email;
				existed.EmailConfirmed = value.EmailConfirmed;
				existed.ExperianceYears = existed.ExperianceYears;
				existed.PhoneNumber = value.PhoneNumber;
				existed.PhoneNumberConfirmed = value.PhoneNumberConfirmed;
				existed.Title = value.Title;
				existed.UserName = value.UserName;

				scope.SaveChanges();
			}
		}

		public void Insert(User value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<User>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(User value)
		{
			throw new NotImplementedException();
		}
	}
}
