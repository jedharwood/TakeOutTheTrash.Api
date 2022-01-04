using Microsoft.AspNetCore.Mvc;
using TakeOutTheTrash.Api.Repositories;
using TakeOutTheTrash.Api.Responses;

namespace TakeOutTheTrash.Api.Controllers
{
    [ApiController]
    [Route("[prefectures]")]
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

            var response = new PrefecturesResponse(
                prefectures);

            return Ok(response);
        }
    }
}
