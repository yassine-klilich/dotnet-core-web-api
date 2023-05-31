using AutoMapper;
using PracticeWebAPI.Models;

namespace PracticeWebAPI.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile() {
            CreateMap<Entities.Pet, Models.Pet>();
            CreateMap<Models.Pet, Entities.Pet>();
            CreateMap<Models.HttpPostPet, Entities.Pet>();
            CreateMap<Models.HttpPutPet, Entities.Pet>();
        }
    }
}
