using Microsoft.AspNetCore.Mvc;
using TakeOutTheTrash.Api.Repositories;

namespace TakeOutTheTrash.Api.Controllers
{
    [ApiController]
    [Route("cities")]
    public class CitiesController : ControllerBase
    {
        private readonly IRepository _repository;

        public CitiesController(IRepository repository)
        {
            _repository = repository; // import logger
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id) // make async once repository is fleshed-out
        {
            var city = _repository.GetCityById(id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}
