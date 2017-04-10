namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Interfaces;
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Content : IEntity, ILocalizedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Key { get; set; }

		public string Description { get; set; }

		public string Keywords { get; set; }

		public string Html { get; set; }

		public DateTime CreatedDate { get; set; }

		public bool IsActive { get; set; }
	}
}
