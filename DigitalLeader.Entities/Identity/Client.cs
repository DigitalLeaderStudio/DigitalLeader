namespace DigitalLeader.Entities.Identity
{
	using DigitalLeader.Entities.Interfaces;
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Client : IEntity, IImageble
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Company { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Title { get; set; }

		public DateTime JoinDate { get; set; }

		public int? ImageId { get; set; }

		public virtual File Image { get; set; }

		public virtual ICollection<Industry> Industries { get; set; }

		public virtual ICollection<Project> Projects { get; set; }
	}
}
