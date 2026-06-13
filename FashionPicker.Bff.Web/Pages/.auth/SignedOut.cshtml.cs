using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FashionPicker.Bff.Web.Pages;

[AllowAnonymous]
public class SignedOut : PageModel
{
    public void OnGet()
    {
        HttpContext.Response.Redirect("/");
    }
}