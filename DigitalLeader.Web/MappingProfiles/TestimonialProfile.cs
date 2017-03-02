namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;

	public class TestimonialProfile : Profile
	{
		public TestimonialProfile()
		{			
			CreateMap<Testimonial, TestimonialViewModel>();

			CreateMap<TestimonialViewModel, Testimonial>();
		}
	}
}