﻿using AutoMapper;
using PracticeWebAPI.Models;

namespace PracticeWebAPI.Profiles
{
    public class PetProfile : Profile
    {
        public PetProfile() {
            CreateMap<Entities.Pet, Models.Pet>();
            CreateMap<Entities.Pet, Models.HttpPatchPet>();
            CreateMap<Models.Pet, Entities.Pet>();
            CreateMap<Models.HttpPostPet, Entities.Pet>();
            CreateMap<Models.HttpPutPet, Entities.Pet>();
            CreateMap<Models.HttpPatchPet, Entities.Pet>();
        }
    }
}
