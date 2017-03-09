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
        public int ServiceCategoryID { get; set; }
    }
}
