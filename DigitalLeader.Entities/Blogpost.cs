namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Blogpost : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public DateTime PublishedDate { get; set; }

		public string Title { get; set; }

		public string Overview { get; set; }

		public string Content { get; set; }

		public string Keywords { get; set; }

		public int ViewsCount { get; set; }

		public bool IsPublished { get; set; }

		public int AuthorId { get; set; }

		public virtual User Author { get; set; }

		public int ServiceId { get; set; }

		public virtual Service Service { get; set; }
	}
}
