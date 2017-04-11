namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;
	using System.Web.Mvc;

	public class ServiceCategoryProfile : Profile
	{
		public ServiceCategoryProfile()
		{
			CreateMap<ServiceCategory, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();

					return new SelectListItem
					{
						Text = src.GetLocalized(x => x.Name, languageId),
						Value = src.ID.ToString()
					};
				});

			CreateMap<ServiceCategory, ServiceCategoryViewModel>()
				.ForMember(vm => vm.ServiceSubcategories, opt => opt.MapFrom(entity => entity.ServiceSubcategories))
				.ForMember(vm => vm.Name, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Name, languageId);
				}))
				.ForMember(vm => vm.Content, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Content, languageId);
				}));


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