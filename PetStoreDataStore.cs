using PracticeWebAPI.Models;

namespace PracticeWebAPI
{
    public class PetStoreDataStore
    {
        public List<Owner> Owners { get; set; }

        public PetStoreDataStore()
        {
            Owners = new List<Owner>()
            {
                new Owner()
                {
                    Id = 1,
                    Name = "Ahmad Omar",
                },
                new Owner()
                {
                    Id = 2,
                    Name = "Yassine Klilich",
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
                    Name = "Simo Filali",
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
