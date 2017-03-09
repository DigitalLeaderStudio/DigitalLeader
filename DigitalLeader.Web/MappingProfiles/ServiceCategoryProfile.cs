namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;
	using System.Collections.Generic;
	using System.Web.Mvc;
	using System.Linq;

	public class ServiceCategoryProfile : Profile
	{
		public ServiceCategoryProfile()
		{
			CreateMap<ServiceCategory, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.Name,
						Value = src.ID.ToString()
					};
				});

			CreateMap<ServiceCategory, ServiceCategoryViewModel>()
				.AfterMap((entity, vm) =>
                {
                    if (entity.ServiceSubcategories != null)
                    {
                        vm.ServiceSubcategories = Mapper.Map<List<ServiceSubcategory>, 
                            List<ServiceSubcategoryViewModel>>(entity.ServiceSubcategories.ToList());
                    }
                });

            CreateMap<ServiceCategoryViewModel, ServiceCategory>()
				.AfterMap((vm, entity) =>
				{
					if (vm.File != null)
					{
						entity.Image = (File)MapperImageConverter.ImageConverter(vm);
					}
				});
		}
	}
}