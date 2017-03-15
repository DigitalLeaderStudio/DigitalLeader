using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace DigitalLeader.ViewModels
{
	public class ServiceSubcategoryViewModel : FileViewModel
	{
		public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<ServiceViewModel> Services{ get; set; }

        [Display(Name = "ServiceCategory")]
        public int ServiceCategoryID { get; set; }
      
        [Display(Name = "ServiceCategories")]
        public List<SelectListItem> ServiceCategoriesSelectList { get; set; }
    }
}
