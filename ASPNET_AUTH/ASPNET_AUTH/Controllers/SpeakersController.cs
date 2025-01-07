using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_AUTH.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class SpeakersController : ControllerBase
    {
        private SpeakersService _service;
        public SpeakersController(SpeakersService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSpeakersAtEvent(int id)
        {
            var result = _service.GetSpeakersAtEvent(id);
            if (result.Count == 0) return NoContent();
            else return Ok(result);
        }
    }
}
