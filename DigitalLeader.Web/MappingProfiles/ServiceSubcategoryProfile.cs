namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;
	using System.Web.Mvc;

	public class ServiceSubcategoryProfile : Profile
	{
		public ServiceSubcategoryProfile()
		{
			CreateMap<ServiceSubcategory, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = src.Name,
						Value = src.ID.ToString()
					};
				});

			CreateMap<ServiceSubcategory, ServiceSubcategoryViewModel>()
				.ForMember(vm => vm.Name, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Name, languageId);
				}))
			   .ForMember(vm => vm.Services, opt => opt.MapFrom(entity => entity.Services));

			CreateMap<ServiceSubcategoryViewModel, ServiceSubcategory>();
			//.AfterMap((vm, entity) =>
			//{
			//	if (vm.File != null)
			//	{
			//		entity.Image = (File)MapperImageConverter.ImageConverter(vm);
			//	}
			//});
		}
	}
}