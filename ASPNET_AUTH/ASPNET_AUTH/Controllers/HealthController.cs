using Microsoft.AspNetCore.Mvc;

namespace ASPNET_AUTH.Controllers;

[ApiController]
[Route("/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult CheckHealth()
    {
        return Ok();
    }
}

