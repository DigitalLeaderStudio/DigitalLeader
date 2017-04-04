namespace DigitalLeader.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;


	public class PromoFormViewModel
	{
		[Display(Name = "First name")]
		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "First name should have 3 to 30 letters")]
		public string FirstName { get; set; }

		[Display(Name = "Last name")]
//		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "Last name should have 3 to 30 letters")]
		public string LastName { get; set; }

		[Display(Name = "Email address")]
		[Required(ErrorMessage = "The email address is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Display(Name = "Company")]
	//	[Required]
		[StringLength(40, MinimumLength = 3, ErrorMessage = "Company name should have 3 to 30 letters")]
		public string Company { get; set; }

		[Display(Name = "Phone number")]
		[Required]
		[StringLength(14, MinimumLength = 8, ErrorMessage = "Phone number should have 8 to 14 characters")]
		public string Phone { get; set; }

		[Display(Name = "Message")]
		[Required]
		[StringLength(1200, MinimumLength = 60, ErrorMessage = "Messageshould have 60 to 1200 letters")]
		public string Message { get; set; }

		public List<string> Errors { get; set; }

		public string RedirectLink { get; set; }
	}
}