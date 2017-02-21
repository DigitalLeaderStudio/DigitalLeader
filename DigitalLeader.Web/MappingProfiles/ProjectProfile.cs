namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels;

	public class ProjectProfile : Profile
	{
		protected override void Configure()
		{
			CreateMap<Project, ProjectViewModel>()
				.ForMember(vm => vm.Client, opt => opt.ResolveUsing(e =>
				{
					return Mapper.Map<ClientViewModel>(e.Client);
				}))
				.ForMember(viewModel => viewModel.ImageId, opt => opt.MapFrom(project => project.ImageId));

			CreateMap<ProjectViewModel, Project>()
				.ForMember(entity => entity.Client, opt => opt.Ignore())
				.ForMember(entity => entity.Image,
					opt => opt.ResolveUsing(MapperImageConverter.ImageConverter));
		}
	}
}