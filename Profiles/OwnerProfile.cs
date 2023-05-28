using AutoMapper;

namespace PracticeWebAPI.Profiles
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<Entities.Owner, Models.OwnerOnly>();
            CreateMap<Entities.Owner, Models.Owner>();
        }
    }
}
