namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities;

	public interface IContentService : IService<Content>
	{
		Content GetByKey(string key);
	}
}
