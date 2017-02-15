namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Technology : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Project> Projects { get; set; }

		public virtual ICollection<User> Employees { get; set; }
	}
}
