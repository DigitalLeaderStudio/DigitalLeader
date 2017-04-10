namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;

	public class ContentProfile : Profile
	{
		public ContentProfile()
		{
			CreateMap<Content, ContentViewModel>()
				.ForMember(vm => vm.Keywords, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Keywords, languageId);
				}))
				.ForMember(vm => vm.Description, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Description, languageId);
				}))
				.ForMember(vm => vm.Html, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Html, languageId);
				}));


			CreateMap<ContentViewModel, Content>();
		}
	}
}