using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPI.Models;

namespace PracticeWebAPI.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly PetStoreDataStore _petStoreDataStore;

        public OwnersController(PetStoreDataStore petStoreDataStore)
        {
            _petStoreDataStore = petStoreDataStore ?? throw new ArgumentNullException(nameof(petStoreDataStore));
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetOwners()
        {
            return Ok(_petStoreDataStore.Owners);
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetOwner(int id)
        {
            Owner? owner = _petStoreDataStore.Owners.Find(x => x.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }
    }
}
