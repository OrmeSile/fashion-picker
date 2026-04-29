using System.Text.Json;
using FashionPicker.Core.Infra.Adapters.LocalCMS;
using FashionPicker.Core.Infra.Models;
using FashionPicker.Core.Infra.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FashionPicker.Core.Api.Controllers;

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