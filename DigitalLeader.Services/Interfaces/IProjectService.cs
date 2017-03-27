namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IProjectService : IService<Project>
	{
         List<Project> GetAllCasestudiesInclude(params Expression<Func<Project, object>>[] includes);
        List<Project> GetAllCaseStudies();
    }
}
