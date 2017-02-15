namespace DigitalLeader.Entities.Identity
{
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Collections.Generic;

	/// <summary>
	/// User is employee
	/// </summary>
	public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IEntity
	{
		public virtual ICollection<Project> Projects { get; set; }

		public virtual ICollection<Service> Services { get; set; }

		public virtual ICollection<Technology> Technologies { get; set; }
	}
}
