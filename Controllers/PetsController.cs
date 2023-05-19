using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<Pet> PostPet(int ownerId, HttpPostPet postPet)
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

        [HttpPut("{petId}")]
        public ActionResult<Pet> PutPet(int ownerId, int petId, HttpPutPet putPet)
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

            pet.Name = putPet.Name;
            pet.Description = putPet.Description;

            return Ok(pet);
        }

        [HttpPatch("{petId}")]
        public ActionResult<Pet> PatchPet(int ownerId, int petId, JsonPatchDocument<HttpPatchPet> jsonPatchDocument)
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

            HttpPatchPet patchPet = new HttpPatchPet()
            {
                Name = pet.Name,
                Description = pet.Description,
            };

            // This ModelState will be applyed on the jsonPatchDocument, not on the patchPet object
            jsonPatchDocument.ApplyTo(patchPet, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Model validation for patchPer object
            if (!TryValidateModel(patchPet))
            {
                return BadRequest(ModelState);
            }

            pet.Name = patchPet.Name;
            pet.Description = patchPet.Description;

            return Ok(pet);
        }

        [HttpDelete("{petId}")]
        public ActionResult<Pet> DeletePet(int ownerId, int petId)
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

            owner.Pets.Remove(pet);

            return NoContent();
        }
    }
}
