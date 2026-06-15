using System.Security.Claims;
using Fashionpicker.ApiGateway.ApiAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

const string corsPolicyName = "AllowCorsForClient";

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.CombineLogs = true;
});
#endif

builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(
            corsPolicyName,
            policy => { policy.WithOrigins("http://localhost:5106").AllowAnyHeader().AllowAnyMethod(); }
        );
    })
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetSection("IdentityProvider")["Authority"];
        options.MetadataAddress = builder.Configuration.GetSection("IdentityProvider")["MetadataAddress"] ?? null!;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetSection("IdentityProvider")["Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetSection("IdentityProvider")["Audience"]
        };
    });
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ApiGateway"))
    .AddTransforms<UserIdExtractorProvider>();

var app = builder.Build();

app.UseCors(corsPolicyName);
app.UseHttpLogging();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/.auth/me-bff", async (HttpContext httpContext) =>
{
    var res = await httpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
}).AllowAnonymous();
app.MapReverseProxy();
app.Run();