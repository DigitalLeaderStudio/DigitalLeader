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

    public class ClientService : IClientService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ClientService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

        public Expression<Func<Client, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Client, object>>[]
                {
                    client => client.Image,
                    client => client.Testimonial,
                    client => client.Industries,
                    client => client.Projects
                };
            }
        }

        public List<Client> GetAll()
		{
            return GetAllInclude(Includes);
		}

        public List<Client> GetAllInclude(params Expression<Func<Client, object>>[] includes)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Client>().AsQueryable();

                if (includes != null)
                {
                    query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.ToList();
            }
        }

        public Client GetById(int id)
		{
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Client>().AsQueryable();

                if (Includes != null)
                {
                    query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.SingleOrDefault(c => c.ID == id);
            }
        }

		public void Update(Client value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext
					.Clients.Find(value.ID);

				existed.Company = value.Company;
				existed.FirstName = value.FirstName;
				existed.Image = value.Image;
				//existed.ImageId = value.ImageId;
				existed.Industries = value.Industries;
				existed.JoinDate = value.JoinDate;
				existed.LastName = value.LastName;
				existed.Projects = value.Projects;
				existed.Title = value.Title;

				scope.SaveChanges();
			}
		}

		public void Insert(Client value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Clients.Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(Client value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Clients.Find(value.ID);

				dbContext.Clients.Remove(existed);

				scope.SaveChanges();
			}
		}

	}
}
