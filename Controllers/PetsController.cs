using Microsoft.AspNetCore.Mvc;
using PracticeWebAPI.Models;

namespace PracticeWebAPI.Controllers
{
    [Route("api/owners/{ownerId}/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Pet>> GetPets(int ownerId)
        {
            Owner? owner = PetStoreDataStore.Instance.Owners.Find(x => x.Id == ownerId);

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner.Pets);
        }

        [HttpGet("{petId}", Name = "GetPet")]
        public ActionResult<Pet> GetPet(int ownerId, int petId)
        {
            Owner? owner = PetStoreDataStore.Instance.Owners.Find(x => x.Id == ownerId);

            if (owner == null)
            {
                return NotFound();
            }

            Pet? pet = owner.Pets.Find(x => x.Id == petId);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpPost]
        public ActionResult<HttpPostPet> PostPet(int ownerId, HttpPostPet postPet)
        {
            Owner? owner = PetStoreDataStore.Instance.Owners.Find(x => x.Id == ownerId);

            if (owner == null)
            {
                return NotFound();
            }

            int id = owner.Pets.Count;
            Pet newPet = new Pet()
            {
                Id = id,
                Name = postPet.Name,
                Description = postPet.Description
            };

            owner.Pets.Add(newPet);

            return CreatedAtRoute("GetPet", new
            {
                ownerId = ownerId,
                petId = id
            }, newPet);
        }
    }
}
