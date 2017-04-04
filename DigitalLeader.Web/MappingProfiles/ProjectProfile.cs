namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using System.Linq;
	using DigitalLeader.ViewModels;

	public class ProjectProfile : Profile
	{
		public ProjectProfile()
		{
			CreateMap<Project, ProjectViewModel>()
				.ForMember(vm => vm.Client, opt => opt.ResolveUsing(e =>
				{
					return Mapper.Map<ClientViewModel>(e.Client);
				}))
				.ForMember(vm => vm.TechnologiesIds, opt => opt.MapFrom(item => item.Technologies.Select(t => t.ID).ToArray()))
				.ForMember(vm => vm.ServicesIds, opt => opt.MapFrom(item => item.Services.Select(s => s.ID).ToArray()))
				.ForMember(vm => vm.ContributorsIds, opt => opt.MapFrom(item => item.Contributors.Select(s => s.Id).ToArray()))
				.ForMember(vm => vm.Logo, opt => opt.Ignore())
				.ForMember(vm => vm.LogoId, opt => opt.MapFrom(project => project.LogoId))
				.ForMember(vm => vm.ImageId, opt => opt.MapFrom(project => project.ImageId));

			CreateMap<ProjectViewModel, Project>()
				.ForMember(entity => entity.Client, opt => opt.Ignore())
				.ForMember(entity => entity.Technologies, opt => opt.Ignore())
				.ForMember(entity => entity.Services, opt => opt.Ignore())
				.ForMember(entity => entity.Contributors, opt => opt.Ignore())
				.ForMember(entity => entity.Logo, opt => opt.Ignore())
				.AfterMap((vm, entity) =>
				{
					if (vm.Logo != null)
					{
						entity.Logo = (File)MapperImageConverter.ImageConverter(vm.Logo);
					}
					if (vm.File != null)
					{
						entity.Image = (File)MapperImageConverter.ImageConverter(vm);
					}
				});
		}
	}
}