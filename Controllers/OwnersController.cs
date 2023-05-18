using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWebAPI.Models;

namespace PracticeWebAPI.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Owner>> GetOwners()
        {
            return Ok(PetStoreDataStore.Instance.Owners);
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetOwner(int id)
        {
            Owner? owner = PetStoreDataStore.Instance.Owners.Find(x => x.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }
    }
}
