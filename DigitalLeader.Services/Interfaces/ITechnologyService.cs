namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;
	using System.Collections.Generic;

	public interface ITechnologyService : IService<Technology>
	{
		List<Technology> GetByIds(int[] ids);
	}
}
