using Microsoft.EntityFrameworkCore;
using PracticeWebAPI.DbContexts;
using PracticeWebAPI.Entities;
using PracticeWebAPI.Services;

namespace PracticeWebAPI.Repositories
{
    public class PetStoreRepository : IPetStoreRepository
    {
        private const int MAX_PAGE_SIZE = 30;
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

        public async Task<bool> OwnerExistsAsync(int ownerId)
        {
            return await _petStoreDbContext.Owners.AnyAsync(o => o.Id == ownerId);
        }

        public void AddOwner(Owner owner)
        {
            _petStoreDbContext.Owners.Add(owner);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _petStoreDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> PetExistsAsync(int petId)
        {
            return await _petStoreDbContext.Pets.AnyAsync(p => p.Id == petId);
        }

        public async Task AddPet(Pet pet)
        {
            await _petStoreDbContext.Pets.AddAsync(pet);
        }

        public void DeletePet(Pet pet)
        {
            _petStoreDbContext.Pets.Remove(pet);
        }

        public async Task<(IEnumerable<Pet>, PaginationMetadata)> GetPetsAsync(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > MAX_PAGE_SIZE)
            {
                pageSize = MAX_PAGE_SIZE;
            }

            IQueryable<Pet> query = _petStoreDbContext.Pets;

            if (string.IsNullOrEmpty(name) == false)
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            
            if (string.IsNullOrEmpty(searchQuery) == false)
            {
                query = query.Where(p => p.Name.Contains(searchQuery) || (p.Description != null && p.Description.Contains(searchQuery)));
            }

            int totalRecords = await query.CountAsync();

            PaginationMetadata paginationMetadata = new PaginationMetadata(totalRecords, pageNumber, pageSize);

            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            List<Pet> collectionToReturn = await query.ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }
    }
}
