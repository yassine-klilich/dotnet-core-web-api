using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPI.Models;
using PracticeWebAPI.Repositories;

namespace PracticeWebAPI.Controllers
{
    [Route("api/owners")]
    [Authorize]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IPetStoreRepository _petStoreRepository;
        private readonly IMapper _mapper;

        public OwnersController(IPetStoreRepository petStoreRepository, IMapper mapper)
        {
            _petStoreRepository = petStoreRepository ?? throw new ArgumentNullException(nameof(petStoreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerOnly>>> GetOwners()
        {
            var owners = await _petStoreRepository.GetOwnersAsync();

            return Ok(_mapper.Map<IEnumerable<OwnerOnly>>(owners));
        }

        [HttpGet("{id}", Name = "GetOwner")]
        public async Task<IActionResult> GetOwner(int id, bool includePets)
        {
            var owner = await _petStoreRepository.GetOwnerAsync(id, includePets);

            if (owner == null)
            {
                return NotFound();
            }

            if (includePets)
            {
                return Ok(_mapper.Map<Owner>(owner));
            }

            return Ok(_mapper.Map<OwnerOnly>(owner));
        }

        [HttpPost]
        public async Task<ActionResult> PostOwner(OwnerOnly owner)
        {
            var _postOwner = _mapper.Map<Entities.Owner>(owner);

            _petStoreRepository.AddOwner(_postOwner);
            
            await _petStoreRepository.SaveChangesAsync();

            var _createdOwner = _mapper.Map<OwnerOnly>(_postOwner);

            return CreatedAtRoute("GetOwner",
                new
                {
                    id = _createdOwner.Id,
                    includePets = false
                },
                _createdOwner);
        }
    }
}
