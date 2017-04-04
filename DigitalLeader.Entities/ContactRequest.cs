namespace DigitalLeader.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ContactRequest : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Message { get; set; }

		public string Email { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Phone { get; set; }

		public string Company { get; set; }

		public string Ip { get; set; }
	}
}
