namespace DigitalLeader.Web.MappingProfiles
{
    using AutoMapper;
    using DigitalLeader.Entities;
    using DigitalLeader.ViewModels;
    using System.Linq;

    public class VacancyProfile : Profile
    {
        public VacancyProfile()
        {
            CreateMap<Vacancy, VacancyViewModel>()
                .ForMember(vm => vm.TechnologiesIds, opt => opt.MapFrom(item => item.Technologies.Select(t => t.ID).ToArray()));

            CreateMap<VacancyViewModel, Vacancy>()
                .ForMember(entity => entity.Technologies, opt => opt.Ignore());
        }
    }
}