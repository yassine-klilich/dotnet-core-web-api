﻿using PracticeWebAPI.Entities;

namespace PracticeWebAPI.Repositories
{
    public interface IPetStoreRepository
    {
        public Task<IEnumerable<Owner>> GetOwnersAsync();

        public Task<Owner?> GetOwnerAsync(int ownerId, bool includePets);

        public Task<IEnumerable<Pet>> GetPetsAsync(int ownerId);

        public Task<Pet?> GetPetAsync(int ownerId, int petId);
    }
}
