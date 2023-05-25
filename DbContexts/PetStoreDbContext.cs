using Microsoft.EntityFrameworkCore;
using PracticeWebAPI.Entities;

namespace PracticeWebAPI.DbContexts
{
    public class PetStoreDbContext : DbContext
    {
        DbSet<Owner> Owners { get; set; }
        DbSet<Pet> Pets { get; set; }

        public PetStoreDbContext(DbContextOptions<PetStoreDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=PetStore; Integrated Security=true; TrustServerCertificate=True");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
