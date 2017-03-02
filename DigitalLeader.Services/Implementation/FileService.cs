namespace DigitalLeader.Services.Implementation
{
	using DigitalLeader.DAL;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Interfaces;
	using EntityFramework.DbContextScope.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class FileService : IFileService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public FileService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public List<File> GetAll()
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();
				return dbContext.Set<File>().ToList();
			}
		}

		public File GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{

				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();
				return dbContext.Set<File>().Find(id);
			}
		}

		public void Update(File value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext
					.Set<File>().Find(value.ID);
				
				existed.Content = value.Content;
				existed.ContentType = value.ContentType;
				existed.FileName = value.FileName;

				scope.SaveChanges();
			}
		}

		public void Insert(File value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<File>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(File value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<File>().Find(value.ID);

				dbContext.Set<File>().Remove(existed);

				scope.SaveChanges();
			}
		}


		public List<File> GetAllInclude(params System.Linq.Expressions.Expression<System.Func<File, object>>[] includes)
		{
			throw new System.NotImplementedException();
		}
	}
}
