using Microsoft.AspNetCore.Mvc;
using TakeOutTheTrash.Api.Repositories;

namespace TakeOutTheTrash.Api.Controllers
{
    [ApiController]
    [Route("prefectures")]
    public class PrefecturesController : ControllerBase
    {
        private readonly IRepository _repository;

        public PrefecturesController(IRepository repository)
        {
            _repository = repository; // import logger
        }

        [HttpGet]
        public IActionResult Get() // make async once repository is fleshed-out
        {
            var prefectures = _repository.GetAllPrefectures();

            if (prefectures.Count == 0)
            {
                return NotFound();
            }

            return Ok(prefectures);
        }

        [HttpGet("{int:id}")]
        public IActionResult Get(int id) // make async once repository is fleshed-out
        {
            var cities = _repository.GetAllCitiesByPrefectureId(id);

            if (cities.Count == 0)
            {
                return NotFound();
            }

            return Ok(cities);
        }
    }
}
