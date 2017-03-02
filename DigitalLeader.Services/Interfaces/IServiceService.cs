namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;
	using System.Collections.Generic;

	public interface IServiceService : IService<Service>
	{
		Service GetByName(string name);

		List<Service> GetByIds(int[] ids);
	}
}
