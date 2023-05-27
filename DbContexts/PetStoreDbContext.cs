using Microsoft.EntityFrameworkCore;
using PracticeWebAPI.Entities;

namespace PracticeWebAPI.DbContexts
{
    public class PetStoreDbContext : DbContext
    {
        DbSet<Owner> Owners { get; set; }
        DbSet<Pet> Pets { get; set; }

        public PetStoreDbContext(DbContextOptions<PetStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(
                new Owner("Ahmad hamidi") {
                    Id = 1,
                    Description = "Description 1"
                },
                new Owner("Yassine KLILICH")
                {
                    Id = 2,
                    Description = "Description 2"
                },
                new Owner("Oumaima Ibrahimi")
                {
                    Id = 3,
                    Description = "Description 3"
                }
            );

            modelBuilder.Entity<Pet>().HasData(
                new Pet("Mimi")
                {
                    Id = 1,
                    OwnerId = 1,
                    Description = "Cat"
                },
                new Pet("Dodi")
                {
                    Id = 2,
                    OwnerId = 1,
                    Description = "Dog"
                },
                new Pet("Alex")
                {
                    Id = 3,
                    OwnerId = 2,
                    Description = "Lion"
                },
                new Pet("Djilali")
                {
                    Id = 4,
                    OwnerId = 3,
                    Description = "Giraffe"
                }
            );

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=PetStore; Integrated Security=true; TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
