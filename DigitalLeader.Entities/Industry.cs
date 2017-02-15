namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Industry
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Client> Clients { get; set; }
	}
}
