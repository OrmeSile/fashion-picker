using FashionPicker.Bff.Web;
using FashionPicker.Bff.Web.ApiClient;
using FashionPicker.Bff.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => { options.AddServerHeader = false; });

var services = builder.Services;
var configuration = builder.Configuration;

var stsServer = configuration["OidcConfiguration:Authority"];

services.AddSecurityHeaderPolicies()
    .SetPolicySelector(ctx =>
        ctx.HttpContext.Request.Path.StartsWithSegments("/api")
            ? ApiSecurityHeadersDefinitions.GetHeaderPolicyCollection(builder.Environment.IsDevelopment())
            : SecurityHeadersDefinitions.GetHeaderPolicyCollection(builder.Environment.IsDevelopment(), stsServer));

services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
    options.Cookie.Name = "__Host-X-XSRF-TOKEN";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

services.AddHttpClient();
services.AddOptions();

services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        configuration.GetSection("OpenIdConnectConfiguration").Bind(options);
        options.Authority = configuration["OidcConfiguration:Authority"];
        options.ClientId = configuration["OidcConfiguration:ClientId"];
        options.ClientSecret = configuration["OidcConfiguration:ClientSecret"];

        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Scope.Add(OpenIdConnectScope.OpenIdProfile);
        options.Scope.Add(OpenIdConnectScope.OfflineAccess);


        options.ResponseType = OpenIdConnectResponseType.Code;

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name"
        };
    });

services.AddControllersWithViews(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

services.AddRazorPages();
services.AddHttpContextAccessor();

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy")).AddTransforms<JwtTransformProvider>();

var app = builder.Build();

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

#if DEBUG
    IdentityModelEventSource.ShowPII = true;
#endif

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseSecurityHeaders();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseNoUnauthorizedRedirect("/.auth");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapNotFound("/{**segment}");

if (app.Environment.IsDevelopment())
{
    var uiDevServer = app.Configuration.GetValue<string>("AngularDevServerUrl");
    if (!string.IsNullOrEmpty(uiDevServer)) app.MapReverseProxy();
}

app.MapFallbackToPage("/_Host");

app.Run();