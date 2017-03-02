namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Testimonial : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Text { get; set; }

		public int? ClientID { get; set; }

		public virtual Client Client { get; set; }
	}
}
