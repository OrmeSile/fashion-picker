using FashionPicker.Core.Adapters;
using FashionPicker.Core.Objects;
using FashionPicker.Infrastructure.Providers;
using Microsoft.AspNetCore.Mvc;

namespace FashionPicker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClothingController(ClothingProvider clothingProvider, ICmsAdapter _): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Clothing>>> GetAllClothing()
    {
         return Ok(await clothingProvider.GetAll());
    }


}