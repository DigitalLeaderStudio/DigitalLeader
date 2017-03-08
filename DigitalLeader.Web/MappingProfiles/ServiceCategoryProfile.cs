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

			CreateMap<ServiceCategory, ServiceCategoryViewModel>();
				//.AfterMap((entity, vm) =>
				//{
				//	if (entity.Services != null)
				//	{
				//		vm.Services = Mapper.Map<List<Service>, List<ServiceViewModel>>(entity.Services.ToList());
				//	}
				//});

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