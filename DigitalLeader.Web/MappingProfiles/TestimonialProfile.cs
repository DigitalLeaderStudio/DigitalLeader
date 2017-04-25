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
			CreateMap<Testimonial, TestimonialWithProjectViewModel>()
				.ForMember(vm => vm.ClientImageID, opt => opt.MapFrom(x => x.Project.Client.ImageId))
				.ForMember(vm => vm.ClientName, opt => opt.ResolveUsing(x =>
				{
					return string.Format("{0} {1}", x.Project.Client.FirstName, x.Project.Client.LastName);
				}))
				.ForMember(vm => vm.ClientTitle, opt => opt.MapFrom(x => x.Project.Client.Title))
				.ForMember(vm => vm.Text, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Text, languageId);
				}));

			CreateMap<Testimonial, TestimonialViewModel>()
				.ForMember(vm => vm.Project, opt => opt.Ignore())
				.ForMember(vm => vm.Text, opt => opt.ResolveUsing(x =>
				{
					var languageId = HttpContext.Current.Request.RequestContext.CurrectLanguageId();
					return x.GetLocalized(item => item.Text, languageId);
				}));


			CreateMap<TestimonialViewModel, Testimonial>();
		}
	}
}