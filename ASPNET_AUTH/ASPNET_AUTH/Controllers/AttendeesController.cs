using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_AUTH.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private AttendeesService _service;
        public AttendeesController(AttendeesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAttendeesAtEvent(int id)
        {
            List<Attendee> result = _service.GetAttendeesAtEvent(id);

            if (result.Count == 0) { return NoContent(); }
            else return Ok(result);
        }
    }
}
