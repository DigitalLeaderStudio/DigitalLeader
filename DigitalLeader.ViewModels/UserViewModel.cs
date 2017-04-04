using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DigitalLeader.ViewModels
{
	public class UserViewModel : FileViewModel
	{
		public int Id { get; set; }

		public int ExperianceYears { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public string Biography { get; set; }

		public string Title { get; set; }

		public int[] TechnologiesIds { get; set; }

		public List<TechnologyViewModel> Technologies { get; set; }

		[Display(Name = "Technologies")]
		public List<SelectListItem> TechnologiesSelectList { get; set; }
	}
}
