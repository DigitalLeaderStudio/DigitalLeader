namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Interfaces;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ServiceSubcategory : IEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Service> Services { get; set; }

        public int ServiceCategoryID { get; set; }

        public virtual ServiceCategory ServiceCategory { get; set; }

    }
}
