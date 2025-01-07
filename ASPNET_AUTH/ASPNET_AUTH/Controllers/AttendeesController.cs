using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

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
        [Route("/Event/{id}")]
        public IActionResult GetAttendeesAtEvent(int id)
        {
            List<Attendee> result = _service.GetAttendeesAtEvent(id);

            if (result.Count == 0) { return NoContent(); }
            else return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAllAttendeesByID(int id)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            int userId = int.Parse(jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);

            if (userId != id) return Unauthorized();
            else
            {
                var result = _service.GetAttendeesByUserId(id);
                if (result is null) { return NoContent(); }
                else return Ok(result);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostAttendeeAsync(int a)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(accessToken) as JwtSecurityToken;
            string userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Attendee newAttendee = new Attendee()
            {
                EventId = a,
                UserId = int.Parse(userId)
            };

            var result = _service.AddAttendee(newAttendee);
            if (result) return Ok(newAttendee);
            else return BadRequest();
        }
    }
}
