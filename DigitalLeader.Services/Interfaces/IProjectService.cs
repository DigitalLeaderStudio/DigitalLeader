namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;
	using System.Collections.Generic;

	public interface IProjectService : IService<Project>
	{
		List<Project> GetAllCaseStudies();
	}
}
