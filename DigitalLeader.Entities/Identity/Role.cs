namespace DigitalLeader.Entities.Identity
{
	using Microsoft.AspNet.Identity.EntityFramework;

	public class Role : IdentityRole<int, UserRole>, IEntity
	{
	}
}
