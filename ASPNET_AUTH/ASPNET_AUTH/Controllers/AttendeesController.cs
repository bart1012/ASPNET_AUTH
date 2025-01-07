using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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

        [HttpPost]
        [Authorize]
        public IActionResult PostAttendee(Attendee input)
        {

            string jwtToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMiIsIm5hbWUiOiJUZXN0IFVzZXIiLCJpYXQiOjE3NjcxOTgwNjcsImV4cCI6MTgwNDA2NzIwMCwiaXNzIjoieW91ci1uYW1lIiwiYXVkIjoieW91ci1hcHAtbmFtZSIsInJvbGVzIjpbXX0.EM4AvEBtWh0E881ioGbnkeaWaCo3D7RAHsHWGptnw-g";
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;
            string userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            Attendee newAttendee = new Attendee()
            {
                EventId = input.EventId,
                UserId = int.Parse(userId)
            };

            var result = _service.AddAttendee(newAttendee);
            if (result) return Ok(newAttendee);
            else return BadRequest();
        }
    }
}
