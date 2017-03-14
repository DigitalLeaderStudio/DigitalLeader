namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
    using DigitalLeader.Entities;
    using DigitalLeader.Entities.Identity;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;

	public class ClientProfile : Profile
	{
		public ClientProfile()
		{
			CreateMap<Client, SelectListItem>().ConvertUsing(
				(src, target) =>
				{
					return new SelectListItem
					{
						Text = string.Format("{0} {1} ({2})", src.FirstName, src.LastName, src.Company),
						Value = src.ID.ToString()
					};
				});

			CreateMap<Client, ClientViewModel>()
				.ForMember(viewModel => viewModel.ImageId, opt => opt.MapFrom(client => client.ImageId));

			CreateMap<ClientViewModel, Client>().AfterMap((vm, entity) =>
            {
                if (vm.File != null)
                {
                    entity.Image = (File)MapperImageConverter.ImageConverter(vm);
                }
            });


		}
	}
}