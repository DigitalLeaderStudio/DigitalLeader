namespace DigitalLeader.ViewModels
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Web.Mvc;

	public class ServiceCategoryViewModel : FileViewModel
	{
		public int ID { get; set; }

		public string Name { get; set; }

		[AllowHtml]
		[UIHint("tinymce_full_compressed")]
		public string Content { get; set; }

        public string CssClass { get; set; }

        public virtual List<ServiceSubcategoryViewModel> ServiceSubcategories { get; set; }
	}
}
