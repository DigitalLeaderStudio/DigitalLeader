namespace DigitalLeader.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ProjectViewModel : FileViewModel
	{
		public ProjectViewModel()
		{
			Client = new ClientViewModel();
		}

		public int ID { get; set; }

		public string Title { get; set; }

		[DataType(DataType.Url)]
		public string ProjectUrl { get; set; }

		[DataType(DataType.MultilineText)]
		public string Kewywords { get; set; }

		[DataType(DataType.MultilineText)]
		public string Overview { get; set; }

		[DataType(DataType.MultilineText)]
		public string Objective { get; set; }

		[DataType(DataType.MultilineText)]
		public string WorkOverview { get; set; }

		[DataType(DataType.MultilineText)]
		public string ResultOverview { get; set; }

		public bool IsCaseStudy { get; set; }

		public bool IsFeatured { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		[Display(Name = "Client")]

		public int? ClientID { get; set; }

		public ClientViewModel Client { get; set; }

		[Display(Name = "Client")]
		public List<SelectListItem> ClientsSelectList { get; set; }

		public int[] TechnologiesIds { get; set; }

		public List<TechnologyViewModel> Technologies { get; set; }

		[Display(Name = "Technologies")]
		public List<SelectListItem> TechnologiesSelectList { get; set; }

		public int[] ServicesIds { get; set; }

		public List<ServiceViewModel> Services { get; set; }

		[Display(Name = "Services")]
		public List<SelectListItem> ServicesSelectList { get; set; }

		public int[] ContributorsIds { get; set; }

		public List<UserViewModel> Contributors { get; set; }

		[Display(Name = "Contributors")]

		public List<SelectListItem> ContributorsSelectList { get; set; }
	}
}
