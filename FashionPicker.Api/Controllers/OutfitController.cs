using Microsoft.AspNetCore.Mvc;

namespace FashionPicker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OutfitController : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    public IActionResult Get()
    {
        return Ok(new{Status = "Ok"});
    }
}