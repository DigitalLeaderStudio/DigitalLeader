namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Interfaces;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Slider : IEntity, IImageble
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string TargetLink { get; set; }

		public string BackgroundStyle { get; set; }

		public bool HasImage { get; set; }

		public int? ImageId { get; set; }

		public File Image { get; set; }
	}
}
