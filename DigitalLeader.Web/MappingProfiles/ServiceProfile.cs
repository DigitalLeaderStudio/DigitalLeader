namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;
	using System.Web.Mvc;

	public class ServiceProfile : Profile
	{
		public ServiceProfile()
		{
			CreateMap<Service, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();

					return new SelectListItem
					{
						Text = src.GetLocalized(x => x.Title, languageId),
						Value = src.ID.ToString()
					};
				});

			CreateMap<Service, ServiceViewModel>()
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.SubTitle, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.SubTitle, languageId);
				}))
				.ForMember(vm => vm.Description, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Description, languageId);
				}))
				.ForMember(vm => vm.Content, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Content, languageId);
				}));

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