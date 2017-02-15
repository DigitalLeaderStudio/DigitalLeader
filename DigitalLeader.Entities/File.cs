namespace DigitalLeader.Entities
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class File
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string FileName { get; set; }

		public string ContentType { get; set; }

		public byte[] Content { get; set; }
	}
}
