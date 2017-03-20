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

    public class VacancyService : BaseService, IVacancyService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public VacancyService(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public Expression<Func<Vacancy, object>>[] Includes
        {
            get
            {
                return new Expression<Func<Vacancy, object>>[]
                {
                    x => x.Technologies,
                };
            }
        }


        public List<Vacancy> GetAllInclude(params Expression<Func<Vacancy, object>>[] includes)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Vacancy>().AsQueryable();

                if (includes != null)
                {
                    query = includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.ToList();
            }
        }

        public List<Entities.Vacancy> GetAll()
        {
            return GetAllInclude(Includes);
        }

        public Vacancy GetById(int id)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                var query = dbContext.Set<Vacancy>().AsQueryable();

                if (Includes != null)
                {
                    query = Includes.Aggregate(query, (curr, incl) => curr.Include(incl));
                }

                return query.SingleOrDefault(c => c.ID == id);
            }
        }

        public void Update(Entities.Vacancy value)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var dbContext = scope.DbContexts
                    .Get<ApplicationDbContext>();

                var existed = dbContext.Set<Vacancy>().SingleOrDefault(s => s.ID == value.ID);

                existed.Title = value.Title;
                existed.ShortDescription = value.ShortDescription;
                existed.Bonuses = value.Bonuses;
                existed.Requirments = value.Requirments;
                existed.Responsibilities = value.Responsibilities;
                existed.WeOffer = value.WeOffer;
                existed.CreatedDate = value.CreatedDate;
                existed.IsPositionOpen = value.IsPositionOpen;

                existed.Technologies = HandleCollection<Technology, int>(
                    existed.Technologies.ToList(),
                    value.Technologies.ToList(),
                    tech => tech.ID,
                    dbContext);

                scope.SaveChanges();
            }
        }

        public void Insert(Vacancy value)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var dbContext = scope.DbContexts
                    .Get<ApplicationDbContext>();

                value.Technologies = HandleCollection<Technology>(value.Technologies.ToList(), dbContext);

                dbContext.Set<Vacancy>().Add(value);

                scope.SaveChanges();
            }
        }

        public void Delete(Entities.Vacancy value)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var dbContext = scope.DbContexts
                    .Get<ApplicationDbContext>();

                var existed = dbContext.Set<Vacancy>().SingleOrDefault(s => s.ID == value.ID);

                dbContext.Set<Vacancy>().Remove(existed);

                scope.SaveChanges();
            }
        }

        public Vacancy GetByName(string name)
        {
            using (var scope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                return dbContext.Set<Vacancy>().SingleOrDefault(s => s.Title == name);
            }
        }

        public List<Vacancy> GetByIds(int[] ids)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var dbContext = scope.DbContexts.Get<ApplicationDbContext>();

                return dbContext.Set<Vacancy>()
                    .Where(t => ids.Contains(t.ID))
                    .ToList();
            }
        }
    }
}
