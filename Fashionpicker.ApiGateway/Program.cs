using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

const string authorizationPolicyName = "AllowIfAuthenticated";

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration.GetSection("IdentityProvider")["Authority"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetSection("IdentityProvider")["Issuer"],
            ValidateAudience = true
        };
    });

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(authorizationPolicyName, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });


builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ApiGateway"));

#if DEBUG
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 512;
    logging.ResponseBodyLogLimit = 512;
    logging.MediaTypeOptions.AddText("multipart/form-data");
    logging.CombineLogs = true;
});
#endif

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapReverseProxy();
app.Run();