namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Services.Localization;
	using DigitalLeader.ViewModels;
	using DigitalLeader.Web.Extensions;
	using System.Web;

	public class TestimonialProfile : Profile
	{
		public TestimonialProfile()
		{
			CreateMap<Testimonial, TestimonialViewModel>()				
				.ForMember(vm => vm.Text, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Text, languageId);
				}));


			CreateMap<TestimonialViewModel, Testimonial>();
		}
	}
}