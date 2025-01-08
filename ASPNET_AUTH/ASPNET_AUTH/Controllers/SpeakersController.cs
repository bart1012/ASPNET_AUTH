using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Route("/events/{id}")]
        public IActionResult GetSpeakersAtEvent(int id)
        {
            var result = _service.GetSpeakersAtEvent(id);
            if (result.Count == 0) return NoContent();
            else return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult PostSpeaker(Speaker speaker)
        {
            var result = _service.PostSpeaker(speaker);

            if (result) return Ok(speaker);
            else return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSpeaker(int id)
        {
            var result = _service.RemoveSpeaker(id);

            if (!result) return NotFound();
            else return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult PutSpeaker(Speaker speaker)
        {
            bool result = _service.UpdateSpeaker(speaker);

            if (!result) return BadRequest();
            else return Ok(speaker);
        }
    }
}
