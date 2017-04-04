namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;

	public class SliderProfile : Profile
	{
		public SliderProfile()
		{
			CreateMap<Slider, SliderViewModel>()
				.ForMember(viewModel => viewModel.ImageId, opt => opt.MapFrom(client => client.ImageId));

			CreateMap<SliderViewModel, Slider>()
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