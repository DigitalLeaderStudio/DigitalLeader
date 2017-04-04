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

	public class ContactRequestService : IContactRequestService
	{
		private readonly IDbContextScopeFactory _dbContextScopeFactory;

		public ContactRequestService(IDbContextScopeFactory dbContextScopeFactory)
		{
			_dbContextScopeFactory = dbContextScopeFactory;
		}

		public Expression<Func<ContactRequest, object>>[] Includes
		{
			get
			{
				return new Expression<Func<ContactRequest, object>>[] { cr => cr };
			}
		}

		public List<ContactRequest> GetAll()
		{
			return GetAllInclude(Includes);
		}

		public List<ContactRequest> GetAllInclude(params Expression<Func<ContactRequest, object>>[] includes)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<ContactRequest>().AsQueryable();

				if (includes != null)
				{
					query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.ToList();
			}
		}

		public ContactRequest GetById(int id)
		{
			using (var scope = _dbContextScopeFactory.CreateReadOnly())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				var query = dbContext.Set<ContactRequest>().AsQueryable();

				if (Includes != null)
				{
					query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
				}

				return query.SingleOrDefault(c => c.ID == id);
			}
		}

		public List<ContactRequest> GetByIds(int[] ids)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

				return dbContext.Set<ContactRequest>()
					.Where(t => ids.Contains(t.ID))
					.ToList();
			}
		}

		public void Update(ContactRequest value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ContactRequest>().SingleOrDefault(t => t.ID == value.ID);

				existed.CreatedDate = value.CreatedDate;
				existed.Company = value.Company;
				existed.Email = value.Email;
				existed.FirstName = value.FirstName;
				existed.LastName = value.LastName;
				existed.Message = value.Message;
				existed.Phone = value.Phone;				

				scope.SaveChanges();
			}
		}

		public void Insert(ContactRequest value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				dbContext.Set<ContactRequest>().Add(value);

				scope.SaveChanges();
			}
		}

		public void Delete(ContactRequest value)
		{
			using (var scope = _dbContextScopeFactory.Create())
			{
				var dbContext = scope.DbContexts
					.Get<ApplicationDbContext>();

				var existed = dbContext.Set<ContactRequest>().SingleOrDefault(t => t.ID == value.ID);

				dbContext.Set<ContactRequest>().Remove(existed);

				scope.SaveChanges();
			}
		}

	}
}
