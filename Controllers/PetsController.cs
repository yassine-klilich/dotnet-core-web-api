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

        [Route("/api/pets")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets(string? name)
        {
            var pets = await _petStoreRepository.GetPetsAsync(name);

            return Ok(_mapper.Map<IEnumerable<Pet>>(pets));
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

            Entities.Pet? pet = await _petStoreRepository.GetPetAsync(ownerId, petId);

            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            return Ok(_mapper.Map<Pet>(pet));
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(int ownerId, HttpPostPet postPet)
        {
            if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
            {
                return NotFound("Owner not found");
            }

            Entities.Pet petEntity = _mapper.Map<Entities.Pet>(postPet);

            petEntity.OwnerId = ownerId;

            await _petStoreRepository.AddPet(petEntity);

            await _petStoreRepository.SaveChangesAsync();

            Pet createdPet = _mapper.Map<Pet>(petEntity);

            return CreatedAtRoute("GetPet", new
            {
                ownerId,
                petId = createdPet.Id
            }, createdPet);
        }

        [HttpPut("{petId}")]
        public async Task<ActionResult<Pet>> PutPet(int ownerId, int petId, HttpPutPet putPet)
        {
            if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
            {
                return NotFound("Owner not found");
            }

            Entities.Pet? petEntity = await _petStoreRepository.GetPetAsync(ownerId, petId);
            if (petEntity == null)
            {
                return NotFound("Pet not found");
            }

            _mapper.Map(putPet, petEntity);

            await _petStoreRepository.SaveChangesAsync();

            return Ok(_mapper.Map<Pet>(petEntity));
        }

        [HttpPatch("{petId}")]
        public async Task<ActionResult<Pet>> PatchPet(int ownerId, int petId, JsonPatchDocument<HttpPatchPet> jsonPatchDocument)
        {
            try
            {
                if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
                {
                    return NotFound("Owner not found");
                }

                Entities.Pet? petEntity = await _petStoreRepository.GetPetAsync(ownerId, petId);
                if (petEntity == null)
                {
                    return NotFound("Pet not found");
                }

                HttpPatchPet petPatch = _mapper.Map<HttpPatchPet>(petEntity);

                // This ModelState will be applied on the jsonPatchDocument,
                // not on the patchPet object
                jsonPatchDocument.ApplyTo(petPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Model validation for patchPer object
                if (!TryValidateModel(petPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(petPatch, petEntity);

                await _petStoreRepository.SaveChangesAsync();

                return Ok(_mapper.Map<Pet>(petEntity));

            }
            catch (Exception ex)
            {
                _logger.LogCritical("Exception while updating a pet", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem happened while handling your request.");
            }
        }

        [HttpDelete("{petId}")]
        public async Task<ActionResult<Pet>> DeletePet(int ownerId, int petId)
        {
            if (!await _petStoreRepository.OwnerExistsAsync(ownerId))
            {
                return NotFound("Owner not found");
            }

            Entities.Pet? petEntity = await _petStoreRepository.GetPetAsync(ownerId, petId);
            if (petEntity == null)
            {
                return NotFound("Pet not found");
            }

            _petStoreRepository.DeletePet(petEntity);

            await _petStoreRepository.SaveChangesAsync();

            _localMail.Send("Pet deleted", $"Pet with id {petEntity.Name} was deleted");

            return NoContent();
        }

    }
}
