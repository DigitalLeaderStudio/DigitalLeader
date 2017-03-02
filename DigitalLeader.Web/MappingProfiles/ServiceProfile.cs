namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;

	public class ServiceProfile : Profile
	{
		public ServiceProfile()
		{
			CreateMap<Service, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.Title,
						Value = src.ID.ToString()
					};
				});

			CreateMap<Service, ServiceViewModel>();

			CreateMap<ServiceViewModel, Service>()
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