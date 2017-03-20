
namespace DigitalLeader.Services.Interfaces
{
    using DigitalLeader.Entities;
    using System.Collections.Generic;

    public interface IVacancyService : IService<Vacancy>
    {
        List<Vacancy> GetByIds(int[] ids);
    }
}
