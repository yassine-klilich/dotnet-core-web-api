using AutoMapper;

namespace PracticeWebAPI.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile() {
            CreateMap<Entities.Pet, Models.Pet>();
        }
    }
}
