namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;

	public interface ISEOService : IService<SEO>
	{
		SEO GetByKey(string key);
	}
}
