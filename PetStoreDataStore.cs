using PracticeWebAPI.Models;

namespace PracticeWebAPI
{
    public class PetStoreDataStore
    {
        public List<Owner> Owners { get; set; }

        public static PetStoreDataStore Instance { get; } = new PetStoreDataStore();

        public PetStoreDataStore()
        {
            Owners = new List<Owner>()
            {
                new Owner()
                {
                    Id = 1,
                    FirstName = "Ahmad",
                    LastName = "Omar"
                },
                new Owner()
                {
                    Id = 2,
                    FirstName = "Yassine",
                    LastName = "Klilich",
                    Pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            Id = 1,
                            Name = "Smoky",
                            Description = "Bird 1"
                        },
                        new Pet()
                        {
                            Id = 2,
                            Name = "Koky",
                            Description = "Bird 2"
                        },
                    }
                },
                new Owner()
                {
                    Id = 3,
                    FirstName = "Simo",
                    LastName = "Filali",
                    Pets = new List<Pet>()
                    {
                        new Pet()
                        {
                            Id = 1,
                            Name = "Yobo",
                            Description = "Rabbit"
                        },
                    }
                },
            };
        }
    }
}
