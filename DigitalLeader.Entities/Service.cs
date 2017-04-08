namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Entities.Interfaces;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Service : IEntity, IImageble, ILocalizedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Title { get; set; }

		public string SubTitle { get; set; }

		public string Description { get; set; }

		public string Content { get; set; }

		public int? ImageId { get; set; }

		public virtual File Image { get; set; }

		public int ServiceSubcategoryID { get; set; }

		public virtual ServiceSubcategory ServiceSubcategory { get; set; }

		public virtual ICollection<Blogpost> Blogposts { get; set; }

		public virtual ICollection<User> Employees { get; set; }

		public virtual ICollection<Project> Projects { get; set; }
	}
}
