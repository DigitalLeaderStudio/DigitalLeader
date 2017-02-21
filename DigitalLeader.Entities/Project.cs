namespace DigitalLeader.Entities
{
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.Entities.Interfaces;
	using System;
	using System.Collections.Generic;

	public class Project : IEntity, IImageble
	{
		public int ID { get; set; }

		public string Title { get; set; }

		public string ProjectUrl { get; set; }

		public string Kewywords { get; set; }

		public string Overview { get; set; }

		public string Objective { get; set; }

		public string WorkOverview { get; set; }

		public string ResultOverview { get; set; }

		public bool IsCaseStudy { get; set; }

		public bool IsFeatured { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public int ClientID { get; set; }

		public virtual Client Client { get; set; }

		public int? ImageId { get; set; }

		public virtual File Image { get; set; }

		public virtual ICollection<User> Contributors { get; set; }

		public virtual ICollection<Technology> Technologies { get; set; }

		public virtual ICollection<Service> Services { get; set; }
	}
}
