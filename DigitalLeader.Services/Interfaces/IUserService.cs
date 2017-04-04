namespace DigitalLeader.Services.Interfaces
{
	using DigitalLeader.Entities.Identity;
	using System.Collections.Generic;

	public interface IUserService: IService<User>
	{
		List<User> GetByIds(int[] ids);

		List<User> GetAllExceptAdmins();
	}
}
