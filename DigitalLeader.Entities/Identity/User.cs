namespace DigitalLeader.Entities.Identity
{
	using DigitalLeader.Entities.Interfaces;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System.Collections.Generic;

	/// <summary>
	/// User is employee
	/// </summary>
	public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IEntity, IImageble
	{
		public string Biography { get; set; }

		public string Title { get; set; }

		public int ExperianceYears { get; set; }

		public int? ImageId { get; set; }

		public virtual File Image { get; set; }

		public virtual ICollection<Project> Projects { get; set; }

		public virtual ICollection<Service> Services { get; set; }

		public virtual ICollection<Technology> Technologies { get; set; }

		public virtual ICollection<Blogpost> Blogposts { get; set; }
	}
}
