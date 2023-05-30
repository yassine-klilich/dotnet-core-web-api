using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPI.Models;
using PracticeWebAPI.Repositories;
using PracticeWebAPI.Services;

namespace PracticeWebAPI.Controllers
{
    [Route("api/owners/{ownerId}/pets")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly ILogger<PetsController> _logger;
        private readonly IMailService _localMail;
        private readonly IPetStoreRepository _petStoreRepository;
        private readonly IMapper _mapper;

        public PetsController(
            ILogger<PetsController> logger,
            IMailService localMail,
            IPetStoreRepository petStoreRepository,
            IMapper mapper
        ) {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMail = localMail ?? throw new ArgumentNullException(nameof(logger));
            _petStoreRepository = petStoreRepository ?? throw new ArgumentNullException(nameof(petStoreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets(int ownerId)
        {
            if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
            {
                return NotFound();
            }
            
            var pets = await _petStoreRepository.GetPetsAsync(ownerId);

            return Ok(_mapper.Map<IEnumerable<Pet>>(pets));
        }

        [HttpGet("{petId}", Name = "GetPet")]
        public async Task<ActionResult<Pet>> GetPet(int ownerId, int petId)
        {
            if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
            {
                return NotFound("Owner not found");
            }

            var result = await _petStoreRepository.GetPetAsync(ownerId, petId);

            if (result == null)
            {
                return NotFound("Pet not found");
            }

            return Ok(_mapper.Map<Pet>(result));
        }

        //[HttpPost]
        //public ActionResult<Pet> PostPet(int ownerId, HttpPostPet postPet)
        //{
        //    Owner? owner = _getOwner(ownerId);

        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }

        //    int id = owner.Pets.Count;
        //    Pet newPet = new Pet()
        //    {
        //        Id = id,
        //        Name = postPet.Name,
        //        Description = postPet.Description
        //    };

        //    owner.Pets.Add(newPet);

        //    return CreatedAtRoute("GetPet", new
        //    {
        //        ownerId = ownerId,
        //        petId = id
        //    }, newPet);
        //}

        //[HttpPut("{petId}")]
        //public ActionResult<Pet> PutPet(int ownerId, int petId, HttpPutPet putPet)
        //{
        //    Owner? owner = _getOwner(ownerId);

        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }

        //    Pet? pet = owner.Pets.Find(x => x.Id == petId);

        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    pet.Name = putPet.Name;
        //    pet.Description = putPet.Description;

        //    return Ok(pet);
        //}

        //[HttpPatch("{petId}")]
        //public ActionResult<Pet> PatchPet(int ownerId, int petId, JsonPatchDocument<HttpPatchPet> jsonPatchDocument)
        //{
        //    try
        //    {
        //        Owner? owner = _getOwner(ownerId);

        //        if (owner == null)
        //        {
        //            return NotFound();
        //        }

        //        Pet? pet = owner.Pets.Find(x => x.Id == petId);

        //        if (pet == null)
        //        {
        //            return NotFound();
        //        }

        //        HttpPatchPet patchPet = new HttpPatchPet()
        //        {
        //            Name = pet.Name,
        //            Description = pet.Description,
        //        };

        //        // This ModelState will be applied on the jsonPatchDocument,
        //        // not on the patchPet object
        //        jsonPatchDocument.ApplyTo(patchPet, ModelState);

        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        // Model validation for patchPer object
        //        if (!TryValidateModel(patchPet))
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        pet.Name = patchPet.Name;
        //        pet.Description = patchPet.Description;

        //        return Ok(pet);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogCritical("Exception while updating a pet", ex);
        //        return StatusCode(StatusCodes.Status500InternalServerError, "A problem happened while handling your request.");
        //    }
        //}

        //[HttpDelete("{petId}")]
        //public ActionResult<Pet> DeletePet(int ownerId, int petId)
        //{
        //    Owner? owner = _getOwner(ownerId);

        //    if (owner == null)
        //    {
        //        return NotFound();
        //    }

        //    Pet? pet = owner.Pets.Find(x => x.Id == petId);

        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    owner.Pets.Remove(pet);
        //    _localMail.Send("Pet deleted", $"Pet with id {pet.Name} was deleted from the owned {owner.Name}");

        //    return NoContent();
        //}

        //private Owner? _getOwner(int ownerId)
        //{
        //    Owner? owner = _petStoreRepository

        //    if (owner == null)
        //    {
        //        _logger.LogInformation($"Can't find an owner with id {ownerId}");
        //    }

        //    return owner;
        //}
    }
}
