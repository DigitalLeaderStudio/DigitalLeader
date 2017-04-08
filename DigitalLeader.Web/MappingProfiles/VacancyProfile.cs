namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Linq;
	using System.Web;

	public class VacancyProfile : Profile
	{
		public VacancyProfile()
		{
			CreateMap<Vacancy, VacancyViewModel>()
				.ForMember(vm => vm.Title, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Title, languageId);
				}))
				.ForMember(vm => vm.ShortDescription, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.ShortDescription, languageId);
				}))
				.ForMember(vm => vm.Bonuses, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Bonuses, languageId);
				}))
				.ForMember(vm => vm.Requirments, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Requirments, languageId);
				}))
				.ForMember(vm => vm.Responsibilities, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Responsibilities, languageId);
				}))
				.ForMember(vm => vm.WeOffer, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.WeOffer, languageId);
				}))
				.ForMember(vm => vm.TechnologiesIds, opt => opt.MapFrom(item => item.Technologies.Select(t => t.ID).ToArray()));

			CreateMap<VacancyViewModel, Vacancy>()
				.ForMember(entity => entity.Technologies, opt => opt.Ignore());
		}
	}
}