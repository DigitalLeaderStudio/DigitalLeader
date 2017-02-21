namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class ClientService : IClientService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ClientService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<Client> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();
				return dbContext.Clients.ToList();
			}
		}

		public Client GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{

				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();
				return dbContext.Clients.Find(id);
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
