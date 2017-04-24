namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Interfaces;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class SEO : IEntity, ILocalizedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Key { get; set; }

		public string Keywords { get; set; }

		public string Description { get; set; }
	}
}
