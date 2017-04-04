namespace DigitalLeader.Web.MappingProfiles
{
	using AutoMapper;
	using DigitalLeader.Entities;
	using DigitalLeader.ViewModels;
	using System.Web.Mvc;

	public class ContactRequestProfile : Profile
	{
		public ContactRequestProfile()
		{			
			CreateMap<ContactRequest, PromoFormViewModel>();

			CreateMap<PromoFormViewModel, ContactRequest>();
			//	.AfterMap((vm, entity) =>
			//	{
			//		if (vm.File != null)
			//		{
			//			entity.Image = (File)MapperImageConverter.ImageConverter(vm);
			//		}
			//	});
		}
	}
}