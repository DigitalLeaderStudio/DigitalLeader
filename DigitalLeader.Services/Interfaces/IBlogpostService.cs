namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;
	using System.Collections.Generic;

	public interface IBlogpostService : IService<Blogpost>
	{
		List<Blogpost> GetAllByService(int serviceId);
	}
}
