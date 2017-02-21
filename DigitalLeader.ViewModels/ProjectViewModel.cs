namespace DigitalLeader.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Web.Mvc;

	public class ProjectViewModel : FileViewModel
	{
		public ProjectViewModel()
		{
			Client = new ClientViewModel();
		}

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

		public ClientViewModel Client { get; set; }

		public List<SelectListItem> Clients { get; set; }

		//public virtual List<User> Contributors { get; set; }

		//public virtual List<Technology> Technologies { get; set; }

		//public virtual List<Service> Services { get; set; }
	}
}
