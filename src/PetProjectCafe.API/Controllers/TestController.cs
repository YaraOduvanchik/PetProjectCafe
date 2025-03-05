using Microsoft.AspNetCore.Mvc;

namespace PetProjectCafe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}