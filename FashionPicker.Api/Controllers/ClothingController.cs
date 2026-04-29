using FashionPicker.Core.Objects;
using FashionPicker.Infrastructure.Providers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FashionPicker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClothingController(ClothingProvider clothingProvider) : ControllerBase
{
    [HttpGet]
    internal async Task<Results<Ok<List<Clothing>>, BadRequest<string>>> GetAllClothing()
    {
        return TypedResults.Ok(await clothingProvider.GetAll());
    }
}