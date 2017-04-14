namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Entities.Interfaces;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Technology : IEntity, ILocalizedEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Project> Projects { get; set; }

		public virtual ICollection<User> Employees { get; set; }

		public virtual ICollection<Vacancy> Vacancies { get; set; }
	}
}
