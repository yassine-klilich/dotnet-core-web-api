using AutoMapper;

namespace PracticeWebAPI.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Entities.Owner, Models.OwnerOnly>();
            CreateMap<Entities.Owner, Models.Owner>();
            CreateMap<Models.OwnerOnly, Entities.Owner>();
            CreateMap<Models.Owner, Entities.Owner>();
        }
    }
}
