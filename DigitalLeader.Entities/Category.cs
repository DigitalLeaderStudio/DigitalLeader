namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Interfaces;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Category : IEntity, IImageble
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }

		public string Content { get; set; }

        public string CssClass { get; set; }

        public int? ImageId { get; set; }

		public virtual File Image { get; set; }

		public virtual ICollection<Blogpost> Blogposts { get; set; }

		public virtual ICollection<Service> Services { get; set; }

	}
}
