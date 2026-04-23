using FashionPicker.Core.Infra.Adapters.LocalCMS;
using FashionPicker.Core.Infra.Models;
using FashionPicker.Core.Infra.Providers;
using Microsoft.AspNetCore.Mvc;

namespace FashionPicker.Core.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClothingController(ClothingProvider clothingProvider, LocalCmsAdapter localCmsAdapter): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Clothing>>> GetAllClothing()
    {
         return Ok(await clothingProvider.GetAll());
    }

    [HttpPost]
    public async Task<ActionResult<Clothing>> CreateClothing()
    {
        var files = await localCmsAdapter.UploadFileAsync(Request);
        return Ok(files);
        // return Ok(await clothingProvider.AddRange([clothing]));
    }
}