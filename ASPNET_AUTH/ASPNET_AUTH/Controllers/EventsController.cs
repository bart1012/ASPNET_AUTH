using ASPNET_AUTH.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_AUTH.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class EventsController : ControllerBase
    {

        private EventsService _eventsService;

        public EventsController(EventsService eventsService)
        {
            _eventsService = eventsService;
        }

        [HttpGet]
        public IActionResult GetEvents()
        {
            return Ok(_eventsService.GetAllEvents());
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventById = _eventsService.GetEventByID(id);

            if (eventById is null) return BadRequest("Event not found.");
            else return Ok(eventById);
        }

        [HttpPost]
        public IActionResult PostEvent(Event newEvent)
        {
            var result = _eventsService.PostEvent(newEvent);
            if (result) return Ok(newEvent);
            else return BadRequest("Id was invalid. Try again.");
        }
    }
}
