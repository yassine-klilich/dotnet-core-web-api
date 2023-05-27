﻿using Microsoft.EntityFrameworkCore;
using PracticeWebAPI.DbContexts;
using PracticeWebAPI.Entities;

namespace PracticeWebAPI.Repositories
{
    public class PetStoreRepository : IPetStoreRepository
    {
        private PetStoreDbContext _petStoreDbContext;

        public PetStoreRepository(PetStoreDbContext petStoreDbContext) {
            _petStoreDbContext = petStoreDbContext ?? throw new ArgumentNullException(nameof(petStoreDbContext));
        }

        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            return await _petStoreDbContext.Owners.OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<Owner?> GetOwnerAsync(int ownerId, bool includePets)
        {
            if (includePets)
            {
                return await _petStoreDbContext.Owners.Include(o => o.Pets).Where(o => o.Id == ownerId).FirstOrDefaultAsync();
            }

            return await _petStoreDbContext.Owners.Where(o => o.Id == ownerId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pet>> GetPetsAsync(int ownerId)
        {
            return await _petStoreDbContext.Pets.Where(p => p.OwnerId == ownerId).ToListAsync();
        }

        public async Task<Pet?> GetPetAsync(int ownerId, int petId)
        {
            return await _petStoreDbContext.Pets.Where(p => p.Id == petId && p.OwnerId == ownerId).FirstOrDefaultAsync();
        }
    }
}